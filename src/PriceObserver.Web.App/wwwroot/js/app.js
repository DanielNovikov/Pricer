window.redirectToPartnerUrl = function (id) {
    var element = document.getElementById(id);
    
    var interval = setInterval(() => {
        
        if (element.hasAttribute("ao_sl_href")) {
            window.location.href = element.href;
            clearInterval(interval);
        }
        
    }, 20);
}