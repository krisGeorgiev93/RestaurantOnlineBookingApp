﻿function statistics() {
    $('#statistics_btn').on('click', function (e) {
        e.preventDefault();
        e.stopPropagation();
        
        if ($('#statistics_box').hasClass('d-none')) {
            $.get('https://localhost:7089/api/statistics', function (data) {
                $('#total_restaurants').text(data.totalRestaurants + " Restaurants");
                $('#total_bookings').text(data.totalBookings + " Bookings");

                $('#statistics_box').removeClass('d-none');

                $('#statistics_btn').text('Hide Statistics');
                $('#statistics_btn').removeClass('btn-primary');
                $('#statistics_btn').addClass('btn-danger');
            });
        } else {
            $('#statistics_box').addClass('d-none');

            $('#statistics_btn').text('Show Statistics');
            $('#statistics_btn').removeClass('btn-danger');
            $('#statistics_btn').addClass('btn-primary');
        }
    });
}