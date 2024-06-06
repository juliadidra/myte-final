

/* Pesquisa departamento

AJAX permite que você atualize uma parte da página web sem ter que recarregar toda a página.

*/
$(document).ready(function () {
    $(".input_search-depart").on("input", function () {
        var searchValue = $(this).val();
        $.ajax({
            url: '@Url.Action("Index", "Departamento")',
            data: { searchString: searchValue },
            success: function (data) {
                $("#departamentoList").html(data);
            }
        });
    });
});



$(document).ready(function () {
    $(".input_search-wbs").on("input", function () {
        var searchValue = $(this).val();
        $.ajax({
            url: '@Url.Action("Index", "Wbs")', // Alterado de "Wbs" para "Departamento"
            data: { searchString: searchValue },
            success: function (data) {
                $("#WbsList").html(data);
            }
        });
    });
});


/*
$(document).ready(function () {
    $(".input_search-func").on("input", function () {
        var searchValue = $(this).val();
        $.ajax({
            url: '@Url.Action("ListaFuncionarios", "Func")',
            data: { searchString: searchValue },
            success: function (data) {
                $("#funcionarioList").html(data);
            }
        });
    });
});
*/