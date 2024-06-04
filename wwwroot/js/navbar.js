
document.addEventListener('DOMContentLoaded', function () {
    var controller = '@ViewContext.RouteData.Values["controller"].ToString()';
    var navItems = document.querySelectorAll('.list');

    navItems.forEach(function (navItem) {
        if (navItem.getAttribute('data-controller') === controller) {
            navItem.classList.add('active');
        }
    });
});

let table = new DataTable('#myTable');