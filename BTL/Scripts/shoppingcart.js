$(document).ready(function () {
    ShowCount();
    
    $('body').on('click', '.btnAddToCart', function (e) {
        e.preventDefault();
        var sizeName = $('#selected-size').text();

        // Kiểm tra nếu sizeName đã được chọn
        if (sizeName === "Chưa chọn") {
            alert("Mời bạn chọn size trước khi thêm vào giỏ hàng");
        } else {
            var id = $(this).data('id');
            var quantity = 1;
            var tQuantity = $('#quantity_value').text();
            if (tQuantity != '') {
                quantity = parseInt(tQuantity);
            }
            $.ajax({
                url: '/shoppingcart/addtocart',
                type: 'POST',
                data: { id: id, quantity: quantity, sizeName: sizeName },
                success: function (rs) {
                    if (rs.Success) {
                        $('#checkout_items').html(rs.count);
                        alert(rs.msg);
                        window.location.href = '/shoppingcart';
                    }
                }
            });
        }
    });


    $('table').on('click', '.btnDeleteFromCart', function (e) {
        e.preventDefault();
        var id = $(this).data('id');
        var conf = confirm("Bạn có muốn xóa sản phẩm khỏi giỏ hàng");
        if (conf == true) {
            $.ajax({
                url: '/shoppingcart/delete',
                type: 'POST',
                data: { id: id },
                success: function (rs) {
                    if (rs.Success) {
                        location.reload("/shoppingcart/index");
                    }
                }
            })
        }
    })
})

function ShowCount() {
    $.ajax({
        url: '/shoppingcart/showcount',
        type: 'GET',
        success: function (rs) {
            $('#checkout_items').html(rs.count);
        }
    })
}
