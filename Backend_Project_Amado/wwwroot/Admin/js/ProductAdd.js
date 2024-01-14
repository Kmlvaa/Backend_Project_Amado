const imgInput = document.querySelector('.img-input');
const dynamicImages = document.querySelector('.dynamicImages');

imgInput.addEventListener('change', (e) => {
    dynamicImages.innerHTML = '';

    for (const file of e.target.files) {
        const imgDiv = document.createElement('div');
        imgDiv.setAttribute('class', 'img-preview');

        const img = document.createElement('img');
        img.style.width = "150px";
        img.style.height = "150px";
        img.style.margin = "10px";
        const blobUrl = URL.createObjectURL(file);
        img.setAttribute('src', blobUrl);
        imgDiv.appendChild(img);

        dynamicImages.appendChild(imgDiv);
    }
});