$(function() {
    getItemCount()
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

    $('#cartButton').on('click', function(){
        $('#shoppingCart').modal();
        $.getJSON({
            url: "../../api/cartitems/products/",
            success: function (response, textStatus, jqXhr) {
                $('#product_cart').html("");
                for (var i = 0; i < response.length; i++){
                    var row =
                        "<p class=\"list-group-item\"> <span id=\"product_info\" data-id=\"" + response[i].productId + "\"></span>" +"  -  quantity: " + response[i].quantity + "</p>"
                    $('#product_cart').append(row);
                }
            },
            error: function (jqXHR, textStatus, errorThrown) {
                console.log("The following error occured: " + textStatus, errorThrown);
            }
        });
    });

    // getCartProducts()
    function getCartProducts() {
        $.getJSON({
            url: "../../api/cartitems/products/",
            success: function (response, textStatus, jqXhr) {
                $('#product_cart').html("");
                for (var i = 0; i < response.length; i++){
                    var row =
                        "<p class=\"list-group-item\"> <span id=\"product_info\" data-id=\"" + response[i].productId + "\"></span>" +"  -  quantity: " + response[i].quantity + "</p>"
                    $('#product_cart').append(row);
                }
            },
            error: function (jqXHR, textStatus, errorThrown) {
                console.log("The following error occured: " + textStatus, errorThrown);
            }
        });
    }
});