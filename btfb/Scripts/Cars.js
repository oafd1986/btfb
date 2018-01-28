//get makes from API
$(document).ready(function () {
  
    $.ajax({
        url: "https://vpic.nhtsa.dot.gov/api/vehicles/getallmakes?format=json",
        type: "GET",
        dataType: "json",
        success: function (result) {
            debugger;
        },
        error: function (xhr, ajaxOptions, thrownError) {
            debugger;
        }
    });

});

function PopulateMakesDropDown(result) {
    // Parse the returned json data
    var opts = $.parseJSON(result);
    // Use jQuery's each to iterate over the opts value
    $.each(opts, function (i, d) {
        // You will need to alter the below to get the right values from your json object.  Guessing that d.id / d.modelName are columns in your carModels data
        $('#emptyDropdown').append('<option value="' + d.ModelID + '">' + d.ModelName + '</option>');
    });

}