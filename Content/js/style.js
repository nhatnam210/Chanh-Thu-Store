//lấy element cha theo selector
function getParent(element, selector) {
    while (element.parentElement) {
        if (element.parentElement.matches(selector))
            return element.parentElement;
		else
            element = element.parentElement
	}
}

//hiển thị menu danh mục con hiện tại theo URL
const x = location.href

var DanhMucCon = document.querySelectorAll('.danh-muc-con')

if (DanhMucCon.length > 0) {
    Array.from(DanhMucCon).forEach((item) => {

        const y = item.dataset.code
        const parent = getParent(item, '.danh-muc')

        if (x.includes(y)) {
            item.classList.add('active')
            parent.classList.add('show')
        } else {
            item.classList.remove('active')

        }
    })

    if (x.endsWith('hang') || x.endsWith('hang/')) {
        const parent = getParent(DanhMucCon[0], '.danh-muc')
        if (parent) {
            DanhMucCon[0].classList.add('active')
            parent.classList.add('show')
        }
    }

}


//Sự kiện tăng giảm số lượng sản phẩm giỏ hàng
const inputQuantity = document.querySelectorAll('.c-input-text')

Array.from(inputQuantity).forEach((item) => {
    const btnDecrease = document.querySelector(`.quantity-box .decrease[data-id="${item.dataset.id}"]`)
    const btnIncrease = document.querySelector(`.quantity-box .increase[data-id="${item.dataset.id}"]`)
    const message = document.querySelector(`.quantity-box .message[data-id="${item.dataset.id}"]`)

    const maxValue = parseInt(item.getAttribute('maxValue'));

    visibilityBtn()

    btnDecrease.addEventListener('click', () => upDownValue(-1));
    btnIncrease.addEventListener('click', () => upDownValue(1));
    item.addEventListener('change', () => visibilityBtn())

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
        message.textContent = "Đã đạt giới hạn tồn kho!"
        setTimeout(() => {
            message.style.visibility = "hidden"
        }, 1000)
    }
})

//Hiển thị thông báo thêm vào giỏ hàng thành công
const listBtnShow = document.querySelectorAll('.add-cart-notify')
const modal = document.querySelector('.modal')
const notifyIcon = document.querySelector('.notify-icon')
const notifyTitle = document.querySelector('.notify-title')

Array.from(listBtnShow).forEach((item) => {
    item.onclick = () => {
        showNotify('Đã thêm vào giỏ hàng!', 'bag-check-outline',1500)
    }
})

function showNotify(title, name, time = 1500) {
    notifyIcon.name = name
    notifyTitle.textContent = title
    modal.classList.add('show')

    setTimeout(() => {
        modal.classList.remove('show')
    }, time)
}


//Hiển thị phí ship

//var ship1 = document.querySelector('#shippingOption1')
//var ship2 = document.querySelector('#shippingOption2')
//var ship3 = document.querySelector('#shippingOption3')
//var money = 0
//ship1.onclick = function () {
//    if (ship1.checked = true) {
//        money = 0
//    }
//}
//ship2.onclick = function () {
//    if (ship2.checked = true) {
//        money = 20000
//    }
//}
//ship3.onclick = function () {
//    if (ship3.checked = true) {
//        money = 60000
        
//    }
//}
var ship = document.querySelectorAll('.custom-control-input')
var total = parseInt(document.getElementById('final-price').innerText)
function formatCash(str) {
    return str.split('').reverse().reduce((prev, next, index) => {
        return ((index % 3) ? next : (next + ',')) + prev
    })
}



ship.forEach(function (value, index) {
    var money = 0
    value.onclick = function () {
        if (index == 0) {
            money = 0
        }
        if (index == 1) {
            money = 20000
        }
        if (index == 2) {
            money = 60000
        }
        
        document.querySelector('.ship').innerHTML = `${formatCash(money.toString())} đ`
        final = money + total
        document.querySelector('#final-price').innerHTML = `${formatCash(final.toString())} đ`
    }
    
})

//ship2.onclick = function () {
//    if (this.checked = true) {
//        alert('adwq')
//    }
//}

