/// <reference path="../jquery-1.10.2.min.js" />


if ($("body").data("title") === "create_user" || $("body").data("title") === "create_city"
    || $("body").data("title") === "create_state" || $("body").data("title") === "create_property") {

    $(document).ready(function () {

        $("#myModal").modal('show');


        var ddlRole = $('#ddlRole');
        ddlRole.append($("<option></option>").val('').html('Please Select Role'));

        $.ajax({
            url: "http://localhost:33146/api/Cascade/GetRole",
            type: "GET",
            dataType: 'json',
            success: function (d) {
                $.each(d, function (i, role) {
                    ddlRole.append($("<option></option>").val(role.roleID).html(role.roleName));
                });
            },
            error: function () {
                alert('Error');
            }
        });
        var ddlCountry = $('#ddlCountry');
        ddlCountry.append($("<option></option>").val('').html('Please Select Country'));
        $.ajax({
            url: 'http://localhost:33146/api/Cascade/GetCountryList',
            type: 'GET',
            dataType: 'json',
            success: function (d) {

                $.each(d, function (i, country) {
                    ddlCountry.append($("<option></option>").val(country.CountryID).html(country.CountryName));
                });
            },
            error: function () {
                alert('Error!');
            }
        });


        $("#ddlCountry").change(function () {
            var CountryId = parseInt($(this).val());
            if (!isNaN(CountryId)) {
                var ddlState = $('#ddlState');
                ddlState.empty();
                ddlState.append($("<option></option>").val('').html('Please wait ...'));
                $.ajax({
                    url: 'http://localhost:33146/api/Cascade/GetState',
                    type: 'GET',
                    dataType: 'json',
                    data: { CountryId: CountryId },
                    success: function (d) {
                        ddlState.empty(); // Clear the please wait
                        ddlState.append($("<option></option>").val('').html('Select State'));
                        $.each(d, function (i, states) {
                            ddlState.append($("<option></option>").val(states.StateID).html(states.StateName));
                        });
                    },
                    error: function () {
                        alert('Errodddddddddddddr!');
                    }
                });
            }

            $("#ddlState").change(function () {
                var StateId = parseInt($(this).val());

                if (!isNaN(StateId)) {
                    var ddlCity = $('#ddlCity');
                    ddlCity.empty();
                    ddlCity.append($("<option></option>").val('').html('Please wait ...'));

                    $.ajax({
                        url: 'http://localhost:33146/api/Cascade/GetCity',
                        type: 'GET',
                        dataType: 'json',
                        data: { stateId: StateId },
                        success: function (d) {

                            ddlCity.empty(); // Clear the please wait
                            ddlCity.append($("<option></option>").val('').html('Select City'));
                            $.each(d, function (i, city) {
                                ddlCity.append($("<option></option>").val(city.CityID).html(city.CityName));
                            });
                        },
                        error: function () {
                            alert('Error!');
                        }
                    });
                }
            });
        });

    });

}

