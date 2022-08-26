$(document).ready(function () {
    'use strict';

    dz_rev_slider_1();
        /* top search in header on click function */
        var quikSearch = $("#quik-search-btn");
        var quikSearchRemove = $("#quik-search-remove");

        quikSearch.on('click', function () {
            $('.dlab-quik-search').animate({ 'width': '100%' });
            $('.dlab-quik-search').delay(500).css({ 'left': '0' });
        });

        quikSearchRemove.on('click', function () {
            $('.dlab-quik-search').animate({ 'width': '0%', 'right': '0' });
            $('.dlab-quik-search').css({ 'left': 'auto' });
        });
        /* top search in header on click function End*/
    

   
$(function () {
    $('.selectpicker').selectpicker();
});

});	/*ready*/
