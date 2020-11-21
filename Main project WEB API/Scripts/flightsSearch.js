
    let allCountries = [];

        $(document).ready(function () {
        $.ajax({
            url: `https://localhost:44326/api/anonymoususer/getallcountries`, //Send Ajax To Get All Countries.
        }).then(function (countries) {
            console.log(countries);
            $.each(countries, function (i, country) {
                $("#OriginCountry").append(
                    `<option value=${country.ID}>${country.CountryName}</option>`
                );
                $("#DestinationCountry").append(
                    `<option value=${country.ID}>${country.CountryName}</option>`
                );
            });
        });
            $.ajax({
        url: `https://localhost:44326/api/anonymoususer/getallflights`, //Send Ajax To Get All Flights.
            }).then(function (flights) {
        buildTable(flights)
    });

});

        function buildTable(flights) {

        console.log(flights);
            $.each(flights, function (i, flight) {
        $("#flights-table").append(
            "<tr>" +
            "<td>" +
            flight.ID +
            "</td>" +
            "<td>" +
            flight.AirlineName +
            "</td>" +
            "<td>" +
            flight.OriginCountry +
            "</td>" +
            "<td>" +
            flight.DepartureTime +
            "</td>" +
            "<td>" +
            flight.DestinationCountry +
            "</td>" +
            "<td>" +
            flight.LandingTime +
            "</td>" +
            "<td>" +
            flight.RemainingTickets +
            "</td>" +
            "<tr>"
        );
});
}
        function searchFlights(fromCtr, toCtr) {
        $.ajax({
            url: `https://localhost:44326/api/anonymoususer/vanillasearchflights?from=${fromCtr}&to=${toCtr}`, // Search Flights By Filters
        }).then(function (flights) {
            $('#flights-table tbody').empty();
            buildTable(flights);
        });
}