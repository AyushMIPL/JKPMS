

using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Web.Mvc;
namespace App.Web.Models
{
    /// <summary>
    /// Class that encapsulates most common parameters sent by DataTables plugin
    /// </summary>
    public class JQueryDataTableParamModel
    {
        /// <summary>
        /// Request sequence number sent by DataTable, same value must be returned in response
        /// </summary>       
        public string sEcho { get; set; }

        /// <summary>
        /// Text used for filtering
        /// </summary>
        public string sSearch { get; set; }

        /// <summary>
        /// Number of records that should be shown in table
        /// </summary>
        public int iDisplayLength { get; set; }

        /// <summary>
        /// First record that should be shown(used for paging)
        /// </summary>
        public int iDisplayStart { get; set; }

        /// <summary>
        /// Number of columns in table
        /// </summary>
        public int iColumns { get; set; }

        /// <summary>
        /// Number of columns that are used in sorting
        /// </summary>
        public int iSortingCols { get; set; }

        /// <summary>
        /// Comma separated list of column names
        /// </summary>
        public string sColumns { get; set; }
        public string eSummary { get; set; }

    }


    /*------------ new Code ----------------------*/
    /// <summary>
    /// Represents the jQuery DataTables request that is sent for server
    /// side processing.
    /// <para>http://datatables.net/usage/server-side</para>
    /// </summary>
    public class JQueryDataTablesModel
    {
        /// <summary>
        /// Gets or sets the information for DataTables to use for rendering.
        /// </summary>
        public int sEcho { get; set; }

        /// <summary>
        /// Gets or sets the display start point.
        /// </summary>
        public int iDisplayStart { get; set; }

        /// <summary>
        /// Gets or sets the number of records to display.
        /// </summary>
        public int iDisplayLength { get; set; }

        /// <summary>
        /// Gets or sets the Global search field.
        /// </summary>
        public string sSearch { get; set; }

        /// <summary>
        /// Gets or sets if the Global search is regex or not.
        /// </summary>
        public bool bRegex { get; set; }

        /// <summary>
        /// Gets or sets the number of columns being display (useful for getting individual column search info).
        /// </summary>
        public int iColumns { get; set; }

        /// <summary>
        /// Gets or sets indicator for if a column is flagged as sortable or not on the client-side.
        /// </summary>
        public ReadOnlyCollection<bool> bSortable_ { get; set; }

        /// <summary>
        /// Gets or sets indicator for if a column is flagged as searchable or not on the client-side.
        /// </summary>
        public ReadOnlyCollection<bool> bSearchable_ { get; set; }

        /// <summary>
        /// Gets or sets individual column filter.
        /// </summary>
        public ReadOnlyCollection<string> sSearch_ { get; set; }

        /// <summary>
        /// Gets or sets if individual column filter is regex or not.
        /// </summary>
        public ReadOnlyCollection<bool> bRegex_ { get; set; }

        /// <summary>
        /// Gets or sets the number of columns to sort on.
        /// </summary>
        public int? iSortingCols { get; set; }

        /// <summary>
        /// Gets or sets column being sorted on (you will need to decode this number for your database).
        /// </summary>
        public ReadOnlyCollection<int> iSortCol_ { get; set; }

        /// <summary>
        /// Gets or sets the direction to be sorted - "desc" or "asc".
        /// </summary>
        public ReadOnlyCollection<string> sSortDir_ { get; set; }

        /// <summary>
        /// Gets or sets the value specified by mDataProp for each column. 
        /// This can be useful for ensuring that the processing of data is independent 
        /// from the order of the columns.
        /// </summary>
        public ReadOnlyCollection<string> mDataProp_ { get; set; }

        public ReadOnlyCollection<SortedColumn> GetSortedColumns()
        {
            if (!iSortingCols.HasValue)
            {
                // Return an empty collection since it's easier to work with when verifying against
                return new ReadOnlyCollection<SortedColumn>(new List<SortedColumn>());
            }

            var sortedColumns = new List<SortedColumn>();
            for (int i = 0; i < iSortingCols.Value; i++)
            {
                sortedColumns.Add(new SortedColumn(mDataProp_[iSortCol_[i]], sSortDir_[i]));
            }

            return sortedColumns.AsReadOnly();
        }
        public string formattedName { get; set; }
    }

    /// <summary>
    /// Represents a sorted column from DataTables.
    /// </summary>
    public class SortedColumn
    {
        private const string Ascending = "asc";

        public SortedColumn(string propertyName, string sortingDirection)
        {
            PropertyName = propertyName;
            Direction = sortingDirection.Equals(Ascending) ? SortingDirection.Ascending : SortingDirection.Descending;
        }

        /// <summary>
        /// Gets the name of the Property on the class to sort on.
        /// </summary>
        public string PropertyName { get; private set; }

        public SortingDirection Direction { get; private set; }
    }

    /// <summary>
    /// Represents the direction of sorting for a column.
    /// </summary>
    public enum SortingDirection
    {
        Ascending,
        Descending
    }

    /////////////////**********************************///////////////////

    public class JQueryDataTablesModelBinder : IModelBinder
    {
        #region Private Variables (Request Keys)

        private const string iDisplayStartKey = "iDisplayStart";
        private const string iDisplayLengthKey = "iDisplayLength";
        private const string iColumnsKey = "iColumns";
        private const string sSearchKey = "sSearch";
        private const string bEscapeRegexKey = "bRegex";
        private const string bSortable_Key = "bSortable_";
        private const string bSearchable_Key = "bSearchable_";
        private const string sSearch_Key = "sSearch_";
        private const string bEscapeRegex_Key = "bRegex_";
        private const string iSortingColsKey = "iSortingCols";
        private const string iSortCol_Key = "iSortCol_";
        private const string sSortDir_Key = "sSortDir_";
        private const string sEchoKey = "sEcho";
        private const string mDataProp_Key = "mDataProp_";

        private ModelBindingContext _bindingContext;

        #endregion

        #region IModelBinder Members

        public object BindModel(ControllerContext controllerContext, ModelBindingContext bindingContext)
        {
            if (bindingContext == null)
            {
                throw new ArgumentNullException("bindingContext");
            }
            _bindingContext = bindingContext;

            //Bind Model
            var dataTablesRequest = new JQueryDataTablesModel();
            dataTablesRequest.sEcho = GetA<int>(sEchoKey);
            if (dataTablesRequest.sEcho <= 0)
            {
                throw new InvalidOperationException("Expected the request to have a sEcho value greater than 0");
            }

            dataTablesRequest.iColumns = GetA<int>(iColumnsKey);
            dataTablesRequest.bRegex = GetA<bool>(bEscapeRegexKey);
            dataTablesRequest.bRegex_ = GetAList<bool>(bEscapeRegex_Key);
            dataTablesRequest.bSearchable_ = GetAList<bool>(bSearchable_Key);
            dataTablesRequest.bSortable_ = GetAList<bool>(bSortable_Key);
            dataTablesRequest.iDisplayLength = GetA<int>(iDisplayLengthKey);
            dataTablesRequest.iDisplayStart = GetA<int>(iDisplayStartKey);
            dataTablesRequest.iSortingCols = GetANullableValue<int>(iSortingColsKey);

            if (dataTablesRequest.iSortingCols.HasValue)
            {
                dataTablesRequest.iSortCol_ = GetAList<int>(iSortCol_Key);
                dataTablesRequest.sSortDir_ = GetStringList(sSortDir_Key);

                if (dataTablesRequest.iSortingCols.Value
                    != dataTablesRequest.iSortCol_.Count)
                {
                    throw new InvalidOperationException(string.Format("Amount of items contained in iSortCol_ {0} do not match the amount specified in iSortingCols which is {1}",
                        dataTablesRequest.iSortCol_.Count, dataTablesRequest.iSortingCols.Value));
                }

                if (dataTablesRequest.iSortingCols.Value
                    != dataTablesRequest.sSortDir_.Count)
                {
                    throw new InvalidOperationException(string.Format("Amount of items contained in sSortDir_ {0} do not match the amount specified in iSortingCols which is {1}",
                        dataTablesRequest.sSortDir_.Count, dataTablesRequest.iSortingCols.Value));
                }
            }
            dataTablesRequest.sSearch = GetString(sSearchKey);
            dataTablesRequest.sSearch_ = GetStringList(sSearch_Key);
            dataTablesRequest.mDataProp_ = GetStringList(mDataProp_Key);

            return dataTablesRequest;
        }

        #endregion

        #region Methods

        /// <summary>
        /// Retrieves an IList of strings from the ModelBindingContext based on the key provided.
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        private ReadOnlyCollection<string> GetStringList(string key)
        {
            var list = new List<string>();
            bool hasMore = true;
            int i = 0;
            while (hasMore)
            {
                var newKey = (key + i.ToString());

                // No need to use a prefix since data tables will not prefix the request names        
                var valueResult = _bindingContext.ValueProvider.GetValue(newKey);

                if (valueResult == null)
                {
                    // If valueResult is still null then we know the value is not in the ModelBindingContext
                    // cease execution of this forloop
                    hasMore = false;
                    continue;
                }

                list.Add((string)valueResult.ConvertTo(typeof(string)));

                i++;
            }

            return list.AsReadOnly();
        }

        private ReadOnlyCollection<T> GetAList<T>(string key) where T : struct
        {
            var list = new List<T>();
            bool hasMore = true;
            int i = 0;
            while (hasMore)
            {
                var newKey = (key + i.ToString());

                var valueResult = _bindingContext.ValueProvider.GetValue(newKey);

                if (valueResult == null)
                {
                    // If valueResult is still null then we know the value is not in the ModelBindingContext
                    // cease execution of this forloop
                    hasMore = false;
                    continue;
                }

                list.Add((T)valueResult.ConvertTo(typeof(T)));
                i++;
            }

            return list.AsReadOnly();
        }

        /// <summary>
        /// Retrieves a string from the ModelBindingContext based on the key provided.
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        private string GetString(string key)
        {
            var valueResult = _bindingContext.ValueProvider.GetValue(key);

            if (valueResult == null)
            {
                return null;
            }

            return (string)valueResult.ConvertTo(typeof(string));
        }

        private T GetA<T>(string key) where T : struct
        {
            var valueResult = _bindingContext.ValueProvider.GetValue(key);

            if (valueResult == null)
            {
                return new T();
            }

            return (T)valueResult.ConvertTo(typeof(T));
        }

        private T? GetANullableValue<T>(string key) where T : struct
        {
            var valueResult = _bindingContext.ValueProvider.GetValue(key);

            if (valueResult == null)
            {
                return null;
            }

            return (T?)valueResult.ConvertTo(typeof(T));
        }

        #endregion
    }

    /////////////////**********************************///////////////////
    /// <summary>
    /// Represents the required data for a response from a request by DataTables.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class JQueryDataTablesResponse<T>
    {
        public JQueryDataTablesResponse(IEnumerable<T> items,
            int totalRecords,
            int totalDisplayRecords,
            int sEcho,
            IEnumerable<T> _eSummary)
        {
            aaData = items;
            eSummary = _eSummary;
            iTotalRecords = totalRecords;
            iTotalDisplayRecords = totalDisplayRecords;
            this.sEcho = sEcho;
        }

        /// <summary>
        /// Sets the Total records, before filtering (i.e. the total number of records in the database)
        /// </summary>
        public int iTotalRecords { get; private set; }

        /// <summary>
        /// Sets the Total records, after filtering 
        /// (i.e. the total number of records after filtering has been applied - 
        /// not just the number of records being returned in this result set)
        /// </summary>
        public int iTotalDisplayRecords { get; private set; }

        /// <summary>
        /// Sets an unaltered copy of sEcho sent from the client side. This parameter will change with each 
        /// draw (it is basically a draw count) - so it is important that this is implemented. 
        /// Note that it strongly recommended for security reasons that you 'cast' this parameter to an 
        /// integer in order to prevent Cross Site Scripting (XSS) attacks.
        /// </summary>
        public int sEcho { get; private set; }

        /// <summary>
        /// Sets the data in a 2D array (Array of JSON objects). Note that you can change the name of this 
        /// parameter with sAjaxDataProp.
        /// </summary>
        public IEnumerable<T> aaData { get; private set; }
        public IEnumerable<T> eSummary { get; private set; }
    }
}