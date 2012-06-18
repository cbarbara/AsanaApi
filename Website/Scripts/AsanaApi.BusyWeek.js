var BusyWeek = function ($) {
	var _startDate = null;
	var _endDate = null;
	var _workspaces = new Array();

	function _onWorkspacesFound(result) {
		var wsDiv = $("#workspaces");
		var content = "<br/><span>Workspaces being searched for Tasks:</span><ul>";
		for (var i = 0; i < result.length; i++) {
			content += "<li data-workspace=\"" + result[i].Id + "\">" + result[i].Name + "</li>";
			_workspaces.push(result[i].Id);
		}
		content += "</ul>";
		wsDiv.children().remove();
		wsDiv.append(content);
		_SearchTasks(true);
	}

	function _SearchTasks(setDivContent) {
		if (setDivContent) {
			$("#tasks").html("<span>Searching for Tasks ....</span><img src='/Images/ajax-loader.gif' alt='loading' />");
		}
		$.ajax({
			url: "/BusyWeek/GetTasks",
			type: "POST",
			dataType: "json",
			traditional: true,
			data: { Workspaces: _workspaces, Start: _startDate, End: _endDate },
			success: function (result) {
				_onTasksFound(result);
			}
		});
	}
	function _onTasksFound(result) {
		console.log(result.CompletedDuringRange);
		console.log(result.StartedAndCompletedDuringRange);
		console.log(result.StartedDuringRange);

		$("#tasks").children().remove();

		if (result.CompletedDuringRange == null) {
			$("#cTasks").html("<em>none</em>");
		}
		else {
			$("#cTasks").html(result.CompletedDuringRange.length);
		}

		if (result.StartedDuringRange == null) {
			$("#sTasks").html("<em>none</em>");
		}
		else {
			$("#sTasks").html(result.StartedDuringRange.length);
		}

		if (result.StartedAndCompletedDuringRange == null) {
			$("#sacTasks").html("<em>none</em>");
		}
		else {
			$("#sacTasks").html(result.StartedAndCompletedDuringRange.length);
		}
	}

	return {
		reinit: function () {
			_ResizeBoxes();
		},
		init: function (startDate, endDate) {
			_startDate = startDate;
			_endDate = endDate;

			$("#workspaces").html("<span>Loading Workspaces ....</span><img src='/Images/ajax-loader.gif' alt='loading' />");

			$.ajax({
				url: "/BusyWeek/GetWorkspaces",
				type: "POST",
				data: null,
				success: function (result) {
					_onWorkspacesFound(result);
				}
			});
		}
	};
} (jQuery);