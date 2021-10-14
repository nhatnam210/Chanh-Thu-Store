
function getParent(element, selector) {
	while (element.parentElement) {
		if (element.parentElement.matches(selector))
			return element.parentElement;
		else
			element = element.parentElement
	}
}

const x = location.href

var DanhMucCon = document.querySelectorAll('.danh-muc-con')

Array.from(DanhMucCon).forEach((item) => {

	const y = item.dataset.code
	const parent = getParent(item,'.danh-muc')

	if (x.includes(y)) {
		item.classList.add('active')
		parent.classList.add('show')
	} else {
		item.classList.remove('active')

	}
})

if (x.endsWith('hang') || x.endsWith('hang/')) {
	const parent = getParent(DanhMucCon[0], '.danh-muc')
	DanhMucCon[0].classList.add('active')
	parent.classList.add('show')
}