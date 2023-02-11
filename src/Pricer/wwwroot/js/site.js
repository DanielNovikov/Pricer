function scrollToView(id) {
    let element = document.getElementById(id);
    element.scrollIntoView({ block: 'start',  behavior: 'smooth' });
}