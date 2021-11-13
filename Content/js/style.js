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

    if (locationHref.endsWith('hang') || locationHref.endsWith('hang/')) {
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
    navItem.forEach((item, index) => {
        if (locationHref.includes(navItemHref[index].href)) {
            item.classList.add('active')
        } else {
            item.classList.remove('active')
        }
    })

    //trường hợp riêng cho trang chi tiết sản phẩm
    if (locationHref.includes('san-pham')) {
        navItem.forEach((item, index) => {
            if (navItemHref[index].href.includes('cua-hang')) {
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
        showNotify('Đã thêm vào giỏ hàng!', 'bag-check-outline', 1500)
        location.reload();
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
var shipMoney;

var finalPrice = document.getElementById('final-price')
if (finalPrice) {
    total = parseInt(finalPrice.innerText)
    //Định dang tiền tệ lần đâu tiên
    finalPrice.innerText = `${formatCash(finalPrice.innerText)} VNĐ`

    var shipInput = getAll('input[name="shipping"]')
    var shipFee = document.getElementById('ship-fee');

    if (shipInput && shipFee) {
        shipInput.forEach(function (item) {
            item.onclick = function () {
                shipMoney = parseInt(item.value)

                shipFee.innerText = `${formatCash(shipMoney.toString())}`
                finalMoney = shipMoney + total
                finalPrice.innerText = `${formatCash(finalMoney.toString())} VNĐ`
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

