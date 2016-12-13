$(function () {
    /** In some case, prevents # link to randomly jump page to the top **/
    $("a[href='#']").click(function (event) {
        event.preventDefault();
    });

    /** Init for popovers**/
    $('[data-toggle="popover"]').popover();

    /** Test-Start **/
    /** Use buttons to naviagte nav-pills for taking the test **/
    $('.nextButton').click(function () {
        $('.nav-pills > .active').next('li').find('a').trigger('click');
    });
    $('.previousButton').click(function () {
        $('.nav-pills > .active').prev('li').find('a').trigger('click');
    });
    /** End of Test-Start **/

    /** Home-Index **/
    setToastr();
    /** End of Home-Index **/
});

function setToastr() {
    toastr.options = {
        "closeButton": true,
        "debug": false,
        "newestOnTop": true,
        "progressBar": true,
        "positionClass": "toast-top-right",
        "preventDuplicates": false,
        "showDuration": "300",
        "hideDuration": "1000",
        "timeOut": "3000",
        "extendedTimeOut": "1000",
        "showEasing": "swing",
        "hideEasing": "linear",
        "showMethod": "fadeIn",
        "hideMethod": "fadeOut"
    };
};