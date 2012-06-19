var BusyWeek = function ($) {
	var _startDate = null;
	var _endDate = null;
	var _workspaces = new Array();

	function _parseDate(aspNetMvcDate) {
		return new Date(parseInt(aspNetMvcDate.substr(6)));
	}

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
		$("#tasks").children().remove();

		if (result.CompletedDuringRange == null) {
			$("#cTasks").html("No tasks created before the start date and completed in this time range.<br/>");
		}
		else {
			$("#cTasks").html(_renderTasks(result.CompletedDuringRange, " tasks created before the start date and completed in this time range."));
		}

		if (result.StartedDuringRange == null) {
			$("#sTasks").html("No tasks created in this time range, but not yet completed.<br/>");
		}
		else {
			$("#sTasks").html(_renderTasks(result.StartedDuringRange, " tasks created in this time range, but not yet completed."));
		}

		if (result.StartedAndCompletedDuringRange == null) {
			$("#sacTasks").html("No tasks created and completed in this time range.<br/>");
		}
		else {
			$("#sacTasks").html(_renderTasks(result.StartedAndCompletedDuringRange, " tasks created and completed in this time range."));
		}
	}
	function _renderTasks(tasks, message) {
		var content = "<span>" + tasks.length.toString() + message + "</span><ul>";
		for (var i = 0; i < tasks.length; i++) {
			content += "<li><div>" + tasks[i].Name + "<br/>";
			content += "Started: " + _parseDate(tasks[i].CreatedAt) + "<br/>";
			if (tasks[i].TimeTaken != null) {
				content += "Finished: " + _parseDate(tasks[i].CompletedAt) + "<br/>";
				content += "Time taken: " + tasks[i].TimeTaken;
			}
			else {
				content += "Unfinished For: " + tasks[i].CurrentRunTime;
			}
			content += "</div></li>";
		}
		return content + "</ul>";
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