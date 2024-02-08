window.onload = function () {
    $("#yearpicker").datepicker({
        format: "yyyy",
        viewMode: "years",
        minViewMode: "years"
    });
    $("#monthpicker").datepicker({
        format: "mm-yyyy",
        startView: "months",
        minViewMode: "months"
    })
    $("#datepicker").datepicker({
        format: "yyyy-mm-dd",
        startView: "weeks",
        minViewMode: "weeks"
    })
    yearMonWeekPicker();
};


function yearMonWeekPicker() {
    var dd2 = document.getElementById("filterdd");
    var ddyear = document.getElementById("yearpicker");
    var ddmonth = document.getElementById("monthpicker");
    var ddweek = document.getElementById("datepicker");
    var weeklabel = document.getElementById("datepicker");
    var enddate = document.getElementById("endDate");
    ddyear.style.display = "none";
    ddmonth.style.display = "none";
    weeklabel.style.display = "none";
    enddate.style.display = "none";
    if (dd2.options[dd2.selectedIndex].text == "Week")
    {
        weeklabel.style.display = "";
        if ($('#datepicker').val() != '') {
            enddate.style.display = "";
        }
        else {
            enddate.style.display = "none";
        }
        ddmonth.value = "";
        ddyear.value = "";

    }
    if (dd2.options[dd2.selectedIndex].text == "Month") {
        ddmonth.style.display = "";
        ddweek.value = "";
        enddate.value = "";
        ddyear.value = "";
    }
    if (dd2.options[dd2.selectedIndex].text == "Year") {
        ddyear.style.display = "";
        ddweek.value = "";
        enddate.value = "";
        ddmonth.value = "";
       
    }
}

function getWeek() {
    var date = document.getElementById("datepicker");
    var enddate = document.getElementById("endDate");
    var newDate = new Date(date.value);
    newDate.setDate(newDate.getDate() + 6);
    enddate.value = newDate.toLocaleDateString("fr-CA");
    enddate.style.display = "";
    enddate.style.background = '#D3D3D3';
}

function ResetValues() {
    var dd2 = document.getElementById("filterdd");
    dd2.selectedIndex = 0;
    yearMonWeekPicker();
}
