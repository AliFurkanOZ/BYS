// Sekme Geçişlerini Yönet
function toggleTab(tabName) {
    if (tabName === 'personel') {
        document.getElementById('personelTab').classList.add('active');
        document.getElementById('ogrenciTab').classList.remove('active');
    } else if (tabName === 'ogrenci') {
        document.getElementById('ogrenciTab').classList.add('active');
        document.getElementById('personelTab').classList.remove('active');
    }
}

// Giriş Formunu Yönet
function submitForm() {
    const username = document.getElementById('username').value;
    const password = document.getElementById('password').value;

    if (username && password) {
        alert(`Giriş Başarılı!\nKullanıcı Adı: ${username}`);
        // Backend'e istek göndermek için buraya AJAX veya Fetch API eklenebilir.
    } else {
        alert('Lütfen kullanıcı adı ve şifre alanlarını doldurunuz.');
    }
}
