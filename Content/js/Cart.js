var cart = {
    init: function () {
        cart.regEvent();
    },
    regEvent: function () {
        $('#btn-update').off('click').on('click', function () {
            var listProduct = $('.txtquantity');
            var cartList = [];
            $.each(listProduct, function (i, item) {
                cartList.push({
                    Soluong: $(item).val(),
                    Sanpham: {
                        MaSanPham: $(item).data('id')
                    }
                });
            });
            $.ajax({
                url: 'Cart/Update',
                data: {
                    cartModel: JSON.stringify(cartList)
                },
                dataType: 'json',
                type: 'POST',
                success: function (res) {
                    if (res.status == true) {
                        //window.location.href = "/gio-hang";
                        location.reload()
                    }
                }
            }) 
        });
        $('.delete-product').off('click').on('click', function (e) {
            e.preventDefault();
            $.ajax({
                data: {id: $(this).data('id')},
                url: 'Cart/Delete',
                
                dataType: 'json',
                type: 'POST',
                success: function (res) {
                    if (res.status == true) {
                        //window.location.href = "/gio-hang";
                        location.reload()
                    }

                }
            })
        })
    }
}
cart.init();