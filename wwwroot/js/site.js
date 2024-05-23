// Please see documentation at https://learn.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.
$(document).ready(function() {
    let cards = $('.onboardingCard');
    let pills = $('.onboardingPills');
    let currentIndex = 0;

    function showCard(index) {
        cards.addClass('d-none');
        cards.eq(index).removeClass('d-none');

        // Remove active class from all pills
        pills.removeClass('active');

        // Add active class to current pill
        pills.eq(index).addClass('active');

        // Hide 'hideOnEnd' buttons and show 'displayOnEnd' buttons when on the last card
        if (index === cards.length - 1) {
            $('.hideOnEnd').addClass('d-none');
            $('.displayOnEnd').removeClass('d-none');
        } else {
            $('.hideOnEnd').removeClass('d-none');
            $('.displayOnEnd').addClass('d-none');
        }
    }

    showCard(currentIndex);

    $('#onboardingActions .nextButton').click(function() {
        currentIndex = (currentIndex + 1) % cards.length;
        showCard(currentIndex);
    });
});