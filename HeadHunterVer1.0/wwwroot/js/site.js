
function toggleNicknameField(isEmployer) {
    let nicknameGroup = document.querySelector(".nickname-group");
    let nicknameLabel = document.getElementById("nicknameLabel");
    let nicknameInput = document.getElementById("nicknameInput");
    if (isEmployer) {
        nicknameLabel.innerText = "Введите название компании";
        nicknameInput.setAttribute("placeholder", "Название компании");
    } else {
        nicknameLabel.innerText = "Ваш никнейм";
        nicknameInput.setAttribute("placeholder", "Никнейм");
    }

    nicknameGroup.classList.remove("d-none");
}
