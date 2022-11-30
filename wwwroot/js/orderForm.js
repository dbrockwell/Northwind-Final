$(function () {
    $('#cart-form').card();
    $.getJSON({
        url: "../../api/cartitems/products/",
        success: function (response, textStatus, jqXhr) {
            $('#product_cart').html("");
            $('#product_cart_total').html("");
            var total = 0;
            for (var i = 0; i < response.length; i++) {
                var productTotal = response[i].qty * response[i].price;
                total += productTotal
                var row =
                    "<p class=\"list-group-item\"> <span id=\"product_info\" data-id=\"" + response[i].Id + "\">" + response[i].name + "  -  $" + response[i].price +
                    " <span class=\"float-right\"> quantity: " + response[i].qty + " - <b>Total: $" + productTotal + "</b>&nbsp; &nbsp;<button class=\"btn btn-outline-danger rounded-circle\" id=\"cartButton\"><i class=\"fas fa-trash\"></i></button>" + "</span></span></p>"
                $('#product_cart').append(row);
            }
            $("#product_cart_total").html(total)
        },
        error: function (jqXHR, textStatus, errorThrown) {
            console.log("The following error occured: " + textStatus, errorThrown);
        }
    });
});