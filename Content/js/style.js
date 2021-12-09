const getAll = document.querySelectorAll.bind(document);
const getOne = document.querySelector.bind(document);
//lấy element cha theo selector
function getParent(element, selector) {
    while (element.parentElement) {
        if (element.parentElement.matches(selector))
            return element.parentElement;
        else
            element = element.parentElement
    }
}

/* ..............................................
 hiển thị menu danh mục con hiện tại theo URL & href
 ................................................. */

const locationHref = location.href
const DanhMucCon = getAll('.danh-muc-con')
var DanhMucConLength
if (DanhMucCon) {
    DanhMucConLength = DanhMucCon.length;
}

if (DanhMucConLength > 0) {
    DanhMucCon.forEach((item) => {
        const parent = getParent(item, '.danh-muc')

        if (locationHref.includes(item.href)) {
            item.classList.add('active')
            parent.classList.add('show')
        } else {
            item.classList.remove('active')
        }
    })

    if (!locationHref.includes('san-pham') && !locationHref.includes('danh-muc-con') && locationHref.includes('cua-hang')) {
        const parentDefault = getParent(DanhMucCon[0], '.danh-muc')
        if (parentDefault) {
            DanhMucCon[0].classList.add('active')
            parentDefault.classList.add('show')
        }
    }
}

/* ..............................................
Active navbar-item theo URL & href
................................................. */

const navItem = getAll('ul.navbar-nav>li')
const navItemHref = getAll('ul.navbar-nav>li>a')

if (navItem && navItemHref) {
    if (locationHref.endsWith('44340') || locationHref.endsWith('44340/')) {
        navItem.forEach((item, index) => {
            item.classList.remove('active')
        })
        navItem[0].classList.add('active')
    } else if (locationHref.includes('cua-hang')) {
        navItem.forEach((item, index) => {
            item.classList.remove('active')
        })
        navItem[2].classList.add('active')
    } else {
        navItem.forEach((item, index) => {
            if (locationHref.includes(navItemHref[index].href) && navItemHref[index].href.length > 0) {
                item.classList.add('active')
            } else {
                item.classList.remove('active')
            }
        })
    }
}

/* ..............................................
Sự kiện tăng giảm số lượng sản phẩm giỏ hàng
 ................................................. */

const inputQuantity = getAll('.c-input-text')

if (inputQuantity) {
    inputQuantity.forEach((item) => {
        const btnDecrease = getOne(`.quantity-box .decrease[data-id="${item.dataset.id}"]`)
        const btnIncrease = getOne(`.quantity-box .increase[data-id="${item.dataset.id}"]`)
        const message = getOne(`.quantity-box .message[data-id="${item.dataset.id}"]`)

        const maxValue = parseInt(item.getAttribute('maxValue'));

        visibilityBtn()
        //item.addEventListener('change', () => visibilityBtn())
        item.addEventListener('input', () => visibilityBtn())

        btnDecrease.addEventListener('click', () => upDownValue(-1));
        btnIncrease.addEventListener('click', () => upDownValue(1));

        //Thay đổi số lượng, ẩn hiện nút khi click tăng giảm
        function upDownValue(option) {
            let itemValue = parseInt(item.value);
            if (option == 1) {
                item.value = itemValue + 1;
                itemValue = parseInt(item.value);
            }
            if (option == -1) {
                item.value = itemValue - 1;
                itemValue = parseInt(item.value);
            }
            visibilityBtn()
        }

        //Ẩn hiện nút theo số lượng
        function visibilityBtn() {
            if (parseInt(item.value) <= 1 || !item.value) {
                item.value = 1;
                btnDecrease.classList.remove('show')
                btnIncrease.classList.add('show')
                message.textContent = ""
            } else if (parseInt(item.value) >= maxValue) {
                if (parseInt(item.value) > maxValue) {
                    MaxValueMess()
                }
                item.value = parseInt(maxValue);
                btnIncrease.classList.remove('show')
                btnDecrease.classList.add('show')
            } else {
                btnDecrease.classList.add('show')
                btnIncrease.classList.add('show')
                message.textContent = ""
            }
        }

        //Hiện thông báo quá số lượng
        function MaxValueMess() {
            message.style.visibility = "visible"
            message.innerText = "Đã đạt giới hạn tồn kho!"
            setTimeout(() => {
                message.style.visibility = "hidden"
            }, 1000)
        }
    })
}

/* ..............................................
Hiển thị thông báo
................................................. */

const listBtnShow = getAll('.add-cart-notify')
const modal = getOne('.modal')
const notifyIcon = getOne('.notify-icon')
const notifyTitle = getOne('.notify-title')

//Hiển thị thông báo thêm vào giỏ hàng thành công
Array.from(listBtnShow).forEach((item) => {
    item.onclick = () => {
        showNotify('Đã thêm vào giỏ hàng!', 'bag-check-outline')
        setTimeout(function () {
            location.reload();
        }, 1500)
    }
})

//Custom Popup thông báo
function showNotify(title, name, time = 1500) {
    notifyIcon.name = name
    notifyTitle.textContent = title
    modal.classList.add('show')

    setTimeout(() => {
        modal.classList.remove('show')
    }, time)
}

/* ..............................................
Hiển thị phí ship
................................................. */

function formatCash(str) {
    return str.split('').reverse().reduce((prev, next, index) => {
        return ((index % 3) ? next : (next + ',')) + prev
    })
}
var total;
var shipMoney = 0;
var voucherPercent = 0;
var finalMoney;
var subMoney;

var finalPrice = document.getElementById('final-price')
if (finalPrice) {
    total = parseInt(finalPrice.innerText)
    //Định dang tiền tệ lần đâu tiên
    finalPrice.innerText = `${formatCash(finalPrice.innerText)} VNĐ`

    var shipInput = getAll('input[name="ship"]')
    var shipFee = document.getElementById('ship-fee');

    var voucherInput = getAll('input[name="mavoucher"]')
    var voucherFee = document.getElementById('voucher-fee');

    if (shipInput && shipFee || voucherInput && voucherFee) {

        function TinhToanGiaCuoi() {
            subMoney = total * (voucherPercent / 100)
            finalMoney = total + shipMoney - total * (voucherPercent / 100)
            shipFee.innerText = `${formatCash(shipMoney.toString())} đ`
            voucherFee.innerText = voucherPercent > 0 ? `-${formatCash(subMoney.toString())} đ (${voucherPercent}%)` : `0%`
            finalPrice.innerText = `${formatCash(finalMoney.toString())} VNĐ`
        }

        // SHIP
        shipInput.forEach(function (item) {
            item.onclick = function () {
                shipMoney = parseInt(item.value)
                TinhToanGiaCuoi()
            }
        })

        //// VOUCHER
        voucherInput.forEach(function (itemVoucher) {
            itemVoucher.onclick = function () {
                voucherPercent = parseInt(itemVoucher.dataset.voucher)
                TinhToanGiaCuoi()
            }
        })

    }
}

/* ..............................................
Ẩn số lượng giỏ hàng khi về 0
 ................................................. */

const quantityCart = getOne('.badge')
if (quantityCart) {
    var countQuantityCart = parseInt(quantityCart.innerText)
    if (countQuantityCart <= 0) {
        quantityCart.style.display = "none"
    } else {
        quantityCart.style.display = "block"
    }
}

/* ..............................................
Hàm Count up number
 ................................................. */

function countUpElement(element, delay = 2000, unit = 10) {
    $(document).ready(function () {
        $(element).counterUp({
            //* đơn vị nhảy
            delay: unit,
            //* delay time
            time: delay
        });
    });
};

/* ..............................................
Bật tắt bảng Ask Options
 ................................................. */

const btnShowAskOptions = getAll(".show-ask-options");
const askOptions = getAll(".ask-options");
const askOptionsNo = getAll(".ask-options--no");
const askOptionsYes = getAll(".ask-options--yes");

//tắt bảng options
function closeAskOptionsShow() {
    const askOptionsShow = getOne(".ask-options.show");
    if (askOptionsShow) {
        askOptionsShow.classList.remove("show");
    }
}

//mở bảng options tại vị trí chỉ định
function openAskOptions(index) {
    if (askOptions) {
        askOptions[index].classList.add("show");
    }
}

//click ra ngoài để tắt bảng options
window.onclick = (e) => {
    if (!e.target.closest(".show-ask-options") && !e.target.closest(".ask-options")) {
        closeAskOptionsShow();
    }
};

/* ..............................................
So sánh điểm user và voucher
 ................................................. */

var voucherContainer = getAll(".voucher__container");
var voucherPoint = getAll(".voucher__point");
var userPoint = getOne(".user-point__value");

//Load các giá trị điểm khi tải trang
countUpElement('.user-point__value', 2000)
countUpElement('.voucher__point span', 1500)
countUpElement('.voucher__value-percent', 2000)


if (userPoint && voucherPoint && voucherContainer
    && btnShowAskOptions && askOptionsYes && askOptionsNo) {

    /*----Hàm cập nhật trạng thái voucher-----*/

    function updateVoucherState() {
        var currentUserPoint = parseInt(userPoint.dataset.point);
        voucherContainer.forEach((item, index) => {
            var voucherPointIndex = parseInt(voucherPoint[index].dataset.point);
            //không đủ điểm
            if (currentUserPoint < voucherPointIndex) {
                item.classList.add("disable-voucher");
                askOptionsYes[index].removeAttribute("href");
            }
        });

        return currentUserPoint;
    }

    //lấy điểm hiện tải sau khi cập nhật
    var currentUserPoint = updateVoucherState();

    //xử lý logic ẩn/hiện voucher
    voucherContainer.forEach((item, index) => {
        var voucherPointIndex = parseInt(voucherPoint[index].dataset.point);
        // đủ điểm
        if (currentUserPoint > voucherPointIndex) {
            //Ấn mở bảng
            btnShowAskOptions[index].addEventListener("click", () => {
                closeAskOptionsShow();
                openAskOptions(index);
            });
            //Ấn "Hủy"
            askOptionsNo[index].addEventListener("click", closeAskOptionsShow);
            //Ấn "Đồng ý"
            askOptionsYes[index].addEventListener("click", () => {
                //Trừ điểm
                currentUserPoint -= voucherPointIndex;
                //Kiêm tra điểm < 0;
                var getCurrentUserPoint = currentUserPoint < 0 ? 0 : currentUserPoint;
                //Gán giá trị sau khi trừ
                userPoint.innerHTML = getCurrentUserPoint;
                userPoint.dataset.point = getCurrentUserPoint;
                closeAskOptionsShow();
                updateVoucherState();
                countUpElement('.user-point__value', 2000);
            });
        }
    });
}

/* ..............................................
Scroll giảm kích thước logo and reverse
 ................................................. */
var currentWidth = $('.logo').css("width");

$(window).scroll(function () {
    var scrollVal = window.scrollY || document.documentElement.scrollTop;
    if (scrollVal >= 52) {
        $('.logo').css("width", "100px");
        //$('.navbar').css("background","#fff")
    } else {
        $('.logo').css("width", currentWidth);
    }
})

/* ..............................................
Paging and sort Jquery
 ................................................. */

//function PagingSort() {
//    $("#basic").on("change", function () {
//        //Getting Value
//        var selValue = $("#basic").val();
//        switch (selValue) {
//            case "0":
//                $('.box-3').css("display", "none")
//                $('.box-2').css("display", "none")
//                $('.box-4').css("display", "none")
//                $('.box-1').css("display", "block")
//                $('.box-5').css("display", "none")

//                break;
//            case "1":
//                $('.box-3').css("display", "none")
//                $('.box-1').css("display", "none")
//                $('.box-4').css("display", "none")
//                $('.box-2').css("display", "block")
//                $('.box-5').css("display", "none")
//                break;
//            case "2":
//                $('.box-2').css("display", "none")
//                $('.box-1').css("display", "none")
//                $('.box-4').css("display", "none")
//                $('.box-3').css("display", "block")
//                $('.box-5').css("display", "none")
//                break;
//            case "3":
//                $('.box-1').css("display", "none")
//                $('.box-2').css("display", "none")
//                $('.box-3').css("display", "none")
//                $('.box-4').css("display", "block")
//                $('.box-5').css("display", "none")
//                break;
//            case "4":
//                $('.box-1').css("display", "none")
//                $('.box-2').css("display", "none")
//                $('.box-3').css("display", "none")
//                $('.box-4').css("display", "none")
//                $('.box-5').css("display", "block")
//                break;
//        }
//    });

//    //render view
//    function RenderProduct(value) {
//        return `<div class="col-lg-4 col-xl-4 col-md-6 col-sm-6 parent">
//                         <div class="products-single fix shadow full-radius">
//                                                <div class="box-img-hover none-radius">
//                                                    <div class="type-lb check-like liked" data-login="${value.isLogin}" data-like="${value.isLiked}" data-id="${value.MaSanPham}">
//                                                                <p class="like bottom-left-radius text-capitalize">Đã thích</p>
//                                                     </div>


//                                                    <img src="${value.HinhChinh}" style="display:none;" class="img-fluid" alt="Image">
//                                                    <div class="img-fit product-mobile" style="background-image: url('${value.HinhChinh}'),url('/Content/images/default.png');">
//                                                    </div>
//                                                    <div class="mask-icon">
//                                                        <ul>
//                                                            <li><a href="/cua-hang/san-pham?id=${value.MaSanPham}" data-toggle="tooltip" data-placement="right" title="Chi tiết"><i class="fas fa-eye"></i></a></li>
//                                                            <li class="check-hide" data-login="${value.isLogin}" >
//                                                                <div class="login-true none">
//                                                                        <a product-data="${value.MaSanPham}" data-like="${value.isLiked}" class="heart-hover js-tongle-like like-true none" data-toggle="tooltip" data-placement="right"
//                                                                           title="Yêu thích">
//                                                                            <i class="fas fa-heart liked-heart-icon"></i>
//                                                                        </a>

//                                                                        <a product-data="${value.MaSanPham}" data-like="${value.isLiked}" class="heart-hover js-tongle-like like-false none" data-toggle="tooltip" data-placement="right"
//                                                                           title="Yêu thích">
//                                                                            <i class="far fa-heart not-like-heart-icon"></i>
//                                                                        </a>
//                                                                </div>
//                                                                <div class="login-false none">
//                                                                        <a href="/dang-nhap" class="heart-hover" data-toggle="tooltip" data-placement="right"
//                                                                           title="Yêu thích">
//                                                                            <i class="far fa-heart not-like-heart-icon"></i>
//                                                                        </a>
//                                                                </div>

//                                                            </li>
//                                                        </ul>
//                                                        <a class="cart add-cart-notify text-capitalize hide-in-mobile font-weight-bold" href="/them-vao-gio?masanpham=${value.MaSanPham}&soluong=1">Thêm Vào Giỏ</a>
//                                                    </div>
//                                                </div>
//                                                <div class="why-text view-row-content">
//                                                    <h4 class="name-product">${value.TenSanPham}</h4>
//                                                    <h5 class="price-product price-sort">${value.Gia}</h5>
//                                                    <a href="/them-vao-gio?masanpham=${value.MaSanPham}&soluong=1" class="btn register hvr-hover text-capitalize add-cart-notify max-width-mobile show-in-mobile font-weight-bold font-size-mobile mt-1">Thêm vào giỏ</a>
//                                                </div>
//                                            </div>
//                    </div>`
//    }

//    //A-Z
//    var as = products.sort((a, b) => a.TenSanPham.toUpperCase() > b.TenSanPham.toUpperCase() ? 1 : -1)
//    const htmls = as.map((value, index) => {
//        return RenderProduct(value);
//    })
//    //Z-A
//    var de = products.sort((a, b) => a.TenSanPham.toUpperCase() < b.TenSanPham.toUpperCase() ? 1 : -1)
//    const htmls2 = de.map((value, index) => {
//        return RenderProduct(value);
//    })

//    //Low-High
//    var priceas = products.sort((a, b) => a.Gia > b.Gia ? 1 : -1)
//    const htmls3 = priceas.map((value, index) => {
//        return RenderProduct(value);
//    })

//    //High-Low
//    var pricede = products.sort((a, b) => a.Gia < b.Gia ? 1 : -1)
//    const htmls4 = pricede.map((value, index) => {
//        return RenderProduct(value);
//    })
//    $(".hihi").html(htmls).paginate()
//    $(".haha").html(htmls2).paginate();
//    $(".huhu").html(htmls3).paginate();
//    $(".hehe").html(htmls4).paginate();
//    $('.example').paginate();

//    const price = $('.price-sort')
//    $.each(price, function (index, value) {

//        value.innerHTML = `${formatCash(value.innerText.toString())} đ`
//    })

//    const isLike = $('.check-like')
//    $.each(isLike, function (index, value) {
//        var checkLogin = value.getAttribute('data-login')
//        var checkLike = value.getAttribute('data-like')
//        if (checkLogin === true.toString()) {
//            if (checkLike === true.toString()) {
//                value.classList.remove('liked')
//            }

//        }

//    })
//    const isHide = $('.check-hide')
//    $.each(isHide, function (index, value) {
//        var checkLogin = value.getAttribute('data-login')
//        var checkLike = value.getAttribute('data-like')
//        if (checkLogin === true.toString()) {
//            $('.login-true').removeClass('none')

//        }
//        else {
//            $('.login-false').removeClass('none')
//        }


//    })
//    const likeTrue = $('.like-true')
//    $.each(likeTrue, function (index, value) {
//        var checkLike = value.getAttribute('data-like')
//        if (checkLike === true.toString()) {
//            value.classList.remove('none')
//        }
//    })
//    const likeFalse = $('.like-false')
//    $.each(likeFalse, function (index, value) {
//        var checkLike = value.getAttribute('data-like')
//        if (checkLike !== true.toString()) {
//            value.classList.remove('none')
//        }
//    })
//}


/* ..............................................
Hiển thị trạng thái sắp xếp hiện tại cuar option
 ................................................. */

$('#basic option').each((i, e) => {
    if (location.href.includes($(e).val())) {
        var selectedSort = $(e).val()
        $(`#basic option[value="${selectedSort}"]`).attr("selected", "")
    }
})