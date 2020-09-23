/*
Include date picker for Safari only
//usage as given below
if (datefield.type!="date"){ //if browser doesn't support input type="date", initialize date picker widget:
    jQuery(function($){ //on document.ready
        $('THE SELECTOR').datepicker();
    })
}

*/

function setDatePicker(elementId) {
    if (datefield.type != "date") { //if browser doesn't support input type="date", initialize date picker widget:
        jQuery(function ($) { //on document.ready
            elementId = '#' + elementId;
            $('#dob').datepicker();
        })
    }
}


var datefield = document.createElement("input")
datefield.setAttribute("type", "date")
if (datefield.type != "date") { //if browser doesn't support input type="date", load files for jQuery UI Date Picker
    document.write('<link href="https://ajax.googleapis.com/ajax/libs/jqueryui/1.12.1/themes/smoothness/jquery-ui.css" rel="stylesheet" type="text/css" />\n')
    document.write('<script src="https://ajax.googleapis.com/ajax/libs/jquery/3.5.1/jquery.min.js"><\/script>\n')
    document.write('<script src="https://ajax.googleapis.com/ajax/libs/jqueryui/1.12.1/jquery-ui.min.js"><\/script>\n')
//    document.write('<link href="https://cdnjs.cloudflare.com/ajax/libs/jqueryui/1.12.1/jquery-ui.min.css" rel="stylesheet" type="text/css" />\n')
//    document.write('<script src="https://ajax.googleapis.com/ajax/libs/jquery/1.4/jquery.min.js"><\/script>\n')
//    document.write('<script src="https://cdnjs.cloudflare.com/ajax/libs/jqueryui/1.12.1/jquery-ui.min.js"><\/script>\n')
}
/*
End
*/

