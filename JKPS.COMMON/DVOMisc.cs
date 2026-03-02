using System;
using System.Collections.Generic;
using System.Text;

namespace JKPS.COMMON
{
    public class DVOMisc
    {
        #region Private Variables
        /// <summary>
        /// 
        /// </summary>
        private int _user_id;
        /// <summary>
        /// 
        /// </summary>
        private string _login_id;
        /// <summary>
        /// 
        /// </summary>
        private string _module;
        /// <summary>
        /// 
        /// </summary>
        private string _dept;
        /// <summary>
        /// 
        /// </summary>
        private int _approval_level;
        /// <summary>
        /// 
        /// </summary>
        private decimal _LevAmount;
        /// <summary>
        /// 
        /// </summary>
        private string _accounttype;
        #endregion
        #region Constructor
        public DVOMisc()
        {
            _user_id = 0;
            _login_id = string.Empty;
            _module = string.Empty;
            _dept = string.Empty;
            _approval_level = 0;
            _LevAmount = 0;
            _accounttype = string.Empty;
        }
          #endregion Constructor
        #region Public Properties
        public int user_id
        {
            get { return _user_id; }
            set { _user_id = value; }
        }
        public string login_id
        {
            get { return _login_id; }
            set { _login_id = value.Trim(); }
        }
        public string module
        {
            get { return _module; }
            set { _module = value.Trim(); }
        }
        public string dept
        {
            get { return _dept; }
            set { _dept = value.Trim(); }
        }
        public int approval_level
        {
            get { return _approval_level; }
            set { _approval_level = value; }
        }
        public decimal LevAmount
        {
            get { return _LevAmount; }
            set { _LevAmount = value; }
        }
        public string accounttype
        {
            get
            {
                return _accounttype;
            }
            set
            {
                _accounttype = value.Trim();
            }
        }
          #endregion Public Properties
    }
}
