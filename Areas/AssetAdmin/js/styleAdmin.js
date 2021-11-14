/* ..............................................
Active navbar-item admin theo URL & href
................................................. */

const linkNavbarMenuAdmin = document.querySelectorAll('.navbar-menu-admin .nav-item a')
const locationHref = location.href

if (linkNavbarMenuAdmin) {
    if (locationHref.includes('danh-muc-con')) {
        linkNavbarMenuAdmin[2].classList.add('active')
    } else {
        linkNavbarMenuAdmin.forEach((item) => {
            if (locationHref.includes(item.href)) {
                item.classList.add('active')
            } else {
                item.classList.remove('active')
            }
        })
    }
   
}
