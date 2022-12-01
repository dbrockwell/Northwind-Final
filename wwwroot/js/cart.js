function getItemCount() {
    $.getJSON({
        url: "../../api/cartitems/count/",
        success: function (response, textStatus, jqXhr) {
            $('#cartItemCount').html(response);
        },
        error: function (jqXHR, textStatus, errorThrown) {
            console.log("The following error occured: " + textStatus, errorThrown);
        }
    });
}

$(function() {
    getItemCount();
    $('#cartButton').on('click', function(){
        $('#shoppingCart').modal();
        updateCart();
    });
    function updateCart() {
        $.getJSON({
            url: "../../api/cartitems/products/",
            success: function (response, textStatus, jqXhr) {
                // console.log(response);
                $('#product_cart').html("");
                $('#product_cart_total').html("");
                var total = 0;
                for (var i = 0; i < response.length; i++){
                    var productTotal = response[i].qty * response[i].price;
                    total += productTotal
                    var row =
                        "<p class=\"list-group-item\"> <span id=\"product_info\" data-id=\"" + response[i].id + "\">" + response[i].name + ":  $" + response[i].price.toFixed(2) + 
                        " <span class=\"float-right\"> quantity: " + response[i].qty + " - <b>Total: $" + productTotal.toFixed(2) + "</b>&nbsp; &nbsp;<button data-id=\"" + response[i].id + "\" class=\"btn btn-outline-danger rounded-circle cartButton\"><i class=\"fas fa-trash\"></i></button>" + "</span></span></p>"
                    $('#product_cart').append(row);
                }
                $("#product_cart_total").html(total.toFixed(2))
            },
            error: function (jqXHR, textStatus, errorThrown) {
                console.log("The following error occured: " + textStatus, errorThrown);
            }
        });
    }
    $('#product_cart').on('click', '.cartButton', function(){
        // alert($(this).data('id'));
        $.ajax({
            url: "../../api/removefromcart/" + $(this).data('id'),
            type: 'delete',
            success: function (response, textStatus, jqXhr) {
                getItemCount();
                updateCart();
            },
            error: function (jqXHR, textStatus, errorThrown) {
                // log the error to the console
                console.log("The following error occured: " + jqXHR.status, errorThrown);
            }
        });
    });
});