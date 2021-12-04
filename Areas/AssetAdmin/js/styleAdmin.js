/* ..............................................
Active navbar-item admin theo URL & href
................................................. */

const linkNavbarMenuAdmin = document.querySelectorAll('.navbar-menu-admin .nav-item a')
const locationHref = location.href

if (linkNavbarMenuAdmin) {
    if (locationHref.includes('loai-san-pham')) {
        linkNavbarMenuAdmin[3].classList.add('active')
    } else if (locationHref.includes('chi-tiet-hoa-don')) {
        linkNavbarMenuAdmin[5].classList.add('active')
    }else {
        linkNavbarMenuAdmin.forEach((item) => {
            if (locationHref.includes(item.href)) {
                item.classList.add('active')
            } else {
                item.classList.remove('active')
            }
        })
    }
}
