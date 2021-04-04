$(document).ready(function () {
    var dates = document.querySelectorAll(".date");
    for (var date of dates) {
        dateFormat(date);
    }
    function dateFormat(date) { 
        var date_str = date.textContent;
        if (date_str != "") {
            var date_split = date_str.split(" ");
            var b_date = date_split[0];
            var s_date = date_split[1];

            var day = b_date.split(".")[0];
            var month = b_date.split(".")[1];
            var year = b_date.split(".")[2];

            if (day.split("")[0] == 0) {
                day = day.split("")[1];
            }

            var hour = s_date.split(":")[0];
            var minute = s_date.split(":")[1];

            var months = [
                'Jan',
                'Feb',
                'Mar',
                'Apr',
                'May',
                'Jun',
                'Jul',
                'Aug',
                'Sep',
                'Oct',
                'Nov',
                'Dec'
            ]

            var format_date = hour + ":" + minute + " " + months[month - 1] + " " + day + ", " + year;
            date.textContent = format_date;
        }       
    }
})