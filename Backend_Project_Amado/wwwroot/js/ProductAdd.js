const imgInput = document.querySelector('.img-input');
const dynamicImages = document.querySelector('.dynamicImages');

imgInput.addEventListener('change', (e) => {

    for (const file of e.target.files) {
        const imgDiv = document.createElement('div');
        imgDiv.setAttribute('class', 'img-preview');
        dynamicImages.style.display = "grid";
        dynamicImages.style.gridTemplateColumns = "auto auto auto auto";
        dynamicImages.style.columnGap = "5px";
        dynamicImages.style.rowGap = "5px";

        const img = document.createElement('img');
        img.style.width = "140px";
        img.style.height = "140px";
        const blobUrl = URL.createObjectURL(file);
        img.setAttribute('src', blobUrl);
        imgDiv.appendChild(img);

        dynamicImages.appendChild(imgDiv);
    }
});