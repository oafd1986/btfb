$(document).ready(function () {
    //populate makes
    $(function () {

        $('#Makes').change(function () {
            var selectedMakeID = this.value;

            $.ajax({
                url: "/Request/Models",
                type: "post",
                data: {

                    makeId: selectedMakeID
                },
                success: function (result) {
                    // refreshes partial view
                    $('#ModelContainer').html(result);

                },
                fail: function () {
                    alert("error");

                }
            });
        });

    });
    //populate models
    $(function () {
        
        $('#Makes').change(function () {
            var selectedMakeID = this.value;

            $.ajax({
                url: "/Request/Models",
                type: "post",
                data: {

                    makeId: selectedMakeID
                },
                success: function (result) {
                    // refreshes partial view
                    $('#ModelContainer').html(result);

                },
                fail: function () {
                    alert("error");

                }
            });
        });
    });
    //get user data
    $(function () {

        $('#UserId').change(function () {
            var selectedUserID = $('#UserId').val();

            $.ajax({
                url: "/Account/GetUserData",
                type: "POST",
                contentType: "application/json; charset=utf-8",
                data: JSON.stringify({ UserId: selectedUserID }),
                dataType: "json",
                success: function (result) {
                    $('#userFirstName').val(result.firstName);
                    $('#userLastName').val(result.lastName);
                    $('#userEmail').val(result.email);
                    $('#userPhone').val(result.phone);


                },

            });
        });
    });
});