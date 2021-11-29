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
        item.addEventListener('change', () => visibilityBtn())

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
                item.value = parseInt(maxValue);
                MaxValueMess()
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
        //location.reload();
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
            shipFee.innerText = `${formatCash(shipMoney.toString())} đ`
            voucherFee.innerText = `${voucherPercent} %`
            finalMoney = total + shipMoney - total * (voucherPercent / 100)
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
                ////Trừ điểm
                //currentUserPoint -= voucherPointIndex;
                ////Kiêm tra điểm < 0;
                //var getCurrentUserPoint = currentUserPoint < 0 ? 0 : currentUserPoint;
                ////Gán giá trị sau khi trừ
                //userPoint.innerHTML = getCurrentUserPoint;
                //userPoint.dataset.point = getCurrentUserPoint;
                //closeAskOptionsShow();
                //updateVoucherState();
                //countUpElement('.user-point__value', 2000);
            });
        }
    });
}

