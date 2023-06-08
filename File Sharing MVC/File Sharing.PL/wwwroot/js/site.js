// Active Navbar Links Listener
var home = document.getElementById('home');
var privacy = document.getElementById('privacy');
var browse = document.getElementById('browse');
var upload = document.getElementById('upload');
var about = document.getElementById('about');
var contacts = document.getElementById('contacts');
var currentPath = window.location.href.split('/');
if (currentPath.length != 5) {
    home.classList.add("navbar-link-active");
}
else if (currentPath[4] == "privacy") {
    privacy.classList.add("navbar-link-active");
}
else if (currentPath[4] == "browse") {
    browse.classList.add("navbar-link-active");
}
else if (currentPath[4] == "upload") {
    upload.classList.add("navbar-link-active");
}
else if (currentPath[4] == "about") {
    about.classList.add("navbar-link-active");
}
else if (currentPath[4] == "contacts") {
    contacts.classList.add("navbar-link-active");
}