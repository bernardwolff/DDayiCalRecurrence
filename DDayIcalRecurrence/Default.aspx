<%@ Page Title="Home Page" Language="C#" AutoEventWireup="true"
    CodeBehind="Default.aspx.cs" Inherits="DDayIcalRecurrence._Default" %>
<html>
<head>
<script src="//ajax.googleapis.com/ajax/libs/jquery/1.9.1/jquery.min.js"></script>

<script src="//ajax.googleapis.com/ajax/libs/jqueryui/1.10.1/jquery-ui.min.js"></script>
<link rel="stylesheet" href="//ajax.googleapis.com/ajax/libs/jqueryui/1.10.1/themes/base/jquery-ui.css" />

<script src="//cdn.jsdelivr.net/jquery.ui.timepicker.addon/1.2/jquery-ui-timepicker-addon.js"></script>
<link rel="stylesheet" href="//cdn.jsdelivr.net/jquery.ui.timepicker.addon/1.2/jquery-ui-timepicker-addon.css" />


<script>
    $(function () {
        var dtPickerFormat = { timeFormat: "hh:mm tt" };
        $('#start').datetimepicker(dtPickerFormat);
        $('#end').datetimepicker(dtPickerFormat);
        $('#until').datepicker();
        $('#freq').change(function (e) {
            if (e.currentTarget.value == 'DAILY') {
                $('#dailyParams').show();
                $('#weeklyParams').hide();
            }
            else if (e.currentTarget.value == 'WEEKLY') {
                $('#weeklyParams').show();
                $('#dailyParams').hide();
            }
        });
        $('#freq').change();
    });
</script>
</head>
<body>
<h1>Add Meeting</h1>
<form id="addMeetingForm" runat="server">
<div><label>Title<input id="title" name="title" runat="server"/></label></div>
<div><label>Start Date/Time<input id="start" name="start" runat="server" /></label></div>
<div><label>End Date/Time<input id="end" name="end" runat="server" /></label></div>
<div><label>Frequency<select id="freq" name="freq" runat="server">
	<option>DAILY</option>
	<option>WEEKLY</option></select></label></div>
<div id="dailyParams">
    <asp:RadioButtonList runat="server" ID="repeat" >
    <asp:ListItem Value="everyDay" checked="true" Text="Repeat Every Day" />
    <asp:ListItem Value="everyWeekday" Text="Every weekday" />
</asp:RadioButtonList>
</div>
<div id="weeklyParams">
Repeats every <input id="interval" name="interval" value="1" runat="server" /> week weeks on
<asp:CheckBoxList runat="server" ID="byweekday">
    <asp:ListItem Value="Monday" Text="Monday" />
    <asp:ListItem value="Tuesday" Text="Tuesday" />
    <asp:ListItem value="Wednesday" Text="Wednesday" />
    <asp:ListItem value="Thursday" Text="Thursday" />
    <asp:ListItem value="Friday" Text="Friday" />
</asp:CheckBoxList>
</div>
<div><label>Repeat until<input id="until" name="until" runat="server" /></label></div>
<div><asp:Button id="submit" runat="server" OnClick="Submit_click" Text="Get Occurrences" /></div>
</form>

The meeting <span id="meetingTitle"></span> will occur on these dates:
<div id="occurrences" runat="server">None so far.</div>

</body>
</html>