$(document).ready(function () {



    $("body").on("keypress", ".only_number", function (e) {

        if (e.which != 8 && e.which != 0 && (e.which < 48 || e.which > 57)) {
            return false;
        }
    });

    $("body").on("keypress", ".only_decimal", function (e) {

        var charCode = (e.which) ? e.which : e.keyCode;
        if (charCode != 46 && charCode > 31 && (charCode < 48 || charCode > 57)) {
            return false;
        }
        else
        {
            var text = $(this).val();

            if (charCode == 46 && (text.indexOf('.') != -1))
            {
                return false;
            }
        
            if ((text.indexOf('.') != -1) && (text.substring(text.indexOf('.')).length > 2)) {
                return false;
            }
            else {
                return true;
            }
        }
       

    });

})

function isEmail(email) {
    var regex = /^([a-zA-Z0-9_\.\-\+])+\@(([a-zA-Z0-9\-])+\.)+([a-zA-Z0-9]{2,4})+$/;
    if (!regex.test(email)) {
        return false;
    } else {
        return true;
    }
}