function employerSummaryChart() {
  var employerName = [];
  var count = [];
  debugger
  $.ajax({
    url: '/Dashboard/employerSummaryAjax',
    type: "GET",
  })
    .done(function (result) {
      debugger
      $.each(result, function (i, names) {
        employerName.push(names.EmployerName);
        count.push(names.Count);
      });
      var employerChart = {
        labels: employerName,
        datasets: [{ fillColor: "lightblue", strokeColor: "blue", data: count }]

      };
      var mybarChartLoc = new Chart(document.getElementById("employerSummary").getContext("2d")).Bar(employerChart);

    })
}

function contributionSummaryChart() {
  var employerName = [];
  var employerContribution = [];
  var contributorContribution = [];
  $.ajax({
    url: '/Dashboard/contributionSummaryAjax',
    type: "GET",
  })
    .done(function (result) {
      debugger
      $.each(result, function (i, names) {
        employerName.push(names.EmployerName);
        employerContribution.push(names.TotalEmployerContribution);
        contributorContribution.push(names.TotalContributorContribution);
      });
      var contributionChart = {
        labels: employerName,
        datasets: [
          {
            //label: "Employer Contribution",
            fillColor: "lightblue",
            strokeColor: "blue",
            data: employerContribution
          }, {
            //label: "Employee’s Contribution",
            fillColor: "lightgray",
            strokeColor: "gray",
            data: contributorContribution
          }
        ]
      };
      var mybarChartLoc = new Chart(document.getElementById("contributorSummary").getContext("2d")).Bar(contributionChart);

    })
}


function showEmployerGraph() {
  employerSummaryChart();
  $("#employerGraph").show();
  $("#employerSummaryStatistics").hide();
}

function showContributionGraph() {
  contributionSummaryChart();
  $("#contributorSummaryStatistics").hide();
  $("#contributorGraph").show();
}
function showEmployerStatistics() {
  $("#employerGraph").hide();
  $("#employerSummaryStatistics").show();
}
function showContributionStatistics() {
  $("#contributorSummaryStatistics").show();
  $("#contributorGraph").hide();
}
function survivor() {
  var tbl = $('#survivor').dataTable({
    "bJQueryUI": true,
    "sPaginationType": "full_numbers",
    "bServerSide": true,
    "bDestroy": true,
    "bAutoWidth": false,
    "sAjaxSource": "/Dashboard/SurvivorAjaxHandler",
    "aoColumns": [
      { "sName": "Name" },
      { "sName": "Relationship" },
      { "sName": "EmployerName" },
      {
        "sName": "Gender",
        "bSearchable": false,
        "bSortable": false,
        "fnRender": function (oObj) {
          if (oObj.aData[3] != "M")
            return '<span > Female </span>';
          else
            return '<span >Male</span>';
        }
      },
      { "sName": "DateOfBirth" },
      { "sName": "TerminationDate" }
    ]
  }).rowGrouping({
    bExpandableGrouping: false,
    iGroupingColumnIndex: 2,
    bHideGroupingColumn: true,
    //asExpandedGroups: [""]
  });
}

function Contributor() {
  debugger
  var contributorTable = $('#tblContributor').dataTable({
    "bJQueryUI": true,
    "sPaginationType": "full_numbers",
    "bServerSide": true,
    "bDestroy": true,
    "bAutoWidth": false,
    "sAjaxSource": "/Dashboard/ContributorAjaxHandler",
    "aoColumns": [
      { "sName": "ApplicationReferenceNo" },
      { "sName": "PresentDistrict" },
      { "sName": "IFSCCode" },
      { "sName": "NameOfTheApplicant" },
      { "sName": "AccountNumber" },
      { "sName": "CurrentStatus" },
      { "sName": "SelectPensionType" }
    ]
  }).rowGrouping({
    bExpandableGrouping: false,
    iGroupingColumnIndex: 6,
    bHideGroupingColumn: true,
    //asExpandedGroups: [""]
  });
}

function Dependant() {
  var dependantTable = $('#tblDependant').dataTable({
    "bJQueryUI": true,
    "sPaginationType": "full_numbers",
    "bServerSide": true,
    "bDestroy": true,
    "bAutoWidth": false,
    "sAjaxSource": "/Dashboard/DependantAjaxHandler",
    "aoColumns": [
      { "sName": "Name" },
      { "sName": "Relationship" },
      { "sName": "EmployerName" },
      {
        "sName": "Gender",
        "bSearchable": false,
        "bSortable": false,
        "fnRender": function (oObj) {
          if (oObj.aData[3] != "M")
            return '<span > Female </span>';
          else
            return '<span >Male</span>';
        }
      },
      { "sName": "DateOfBirth" },
      { "sName": "TerminationDate" }
    ]
  }).rowGrouping({
    bExpandableGrouping: false,
    iGroupingColumnIndex: 2,
    bHideGroupingColumn: true,
    //asExpandedGroups: [""]
  });
}