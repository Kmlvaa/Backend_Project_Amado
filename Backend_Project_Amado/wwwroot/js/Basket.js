let productContainer = document.querySelector(".product-container");
let basketContainer = document.querySelector(".basket-container");
let ProductsList = [];

fetch("https://dummyjson.com/products")
    .then((res) => res.json())
    .then((data) => {
        ProductsList = data.products;
        console.log(ProductsList);
        setData();
    });


function setData() {
    ProductsList.forEach((product) => {
        const { id, title, description, price, brand, category, images } = product;
        let productCard = generateProductCard(id, title, description, price, brand, category, images);
        productContainer.innerHTML += productCard;
    });
}
document.querySelectorAll(".add-to-basket-btn").forEach((btn) => {
    btn.onclick = (e) => {
        console.log('hi');
        let id = e.target.getAttribute("data-id");
        let basket = JSON.parse(localStorage.getItem("basket"));
        if (basket == null) {
            localStorage.setItem("basket", JSON.stringify([{ id, count: 1 }]));
        } else {
            if (basket.some((x) => x.id == id)) return;
            basket.push({ id, count: 1 });
            localStorage.setItem("basket", JSON.stringify(basket));
        }
        renderBasketSection();
    };
});

function generateProductCard(id, title, description, price, brand, category, images) {
    let productCard =
        `<div class="col card-container">
      <div class="card" style="width: 18rem">
      <div class="card-body">
        <div class="image">
          <img src="${images[0]}" class="card-img-top" alt="${title}"/>
        </div>
        <div class="inside"><h5 class="card-title">${title} / ${brand}</h5>
        <p>Description: ${description}</p>
        <p>Category: ${category}</p>
        <p class="card-text">Price: <span class="price">${price.toFixed(2)}$</span></p>
        </div>
        <button data-id=${id} class="add-to-basket-btn  btn btn-primary">Add to basket</button>
        </div>
      </div>
    </div>`;
    return productCard;
}

function renderBasketSection() {
    let basket = JSON.parse(localStorage.getItem("basket"));
    if (basket == null) {
        totalPrice.innerHTML = 0;
        return;
    }

    basketContainer.innerHTML = "";
    cardButton.innerHTML = "Cart (";

    let total = 0;
    let item_total = 0;
    var total_count = 0;


    basket.forEach((x) => {
        let foundProduct = products.find((p) => p.id == x.id);
        if (foundProduct == null) return;
        total += x.count * foundProduct.price;
        item_total = x.count * foundProduct.price;
        total_count += x.count
        let basketItem = `
      <div class="basket-item">
        <span>${foundProduct.name}</span> 
        <span>(${foundProduct.price})</span>
        <div class="count-btns">
        <button class="btn btn-secondary increase-btn" style="background-color:lightgrey; border:none; color:black; border-radius: 3px;" data-id=${x.id}>+</button>
        <span class="count">${x.count}</span>
        <button class="btn btn-secondary decrease-btn" style="background-color:lightgrey; border:none; color:black; border-radius: 3px;" data-id=${x.id}>-</button>
        </div>
        <button class="btn btn-danger delete-btn" style="width:40px;" data-id=${x.id}>X</button>
        <div>$<span>${item_total.toFixed(2)}</span></div>
    </div>`;

        basketContainer.innerHTML += basketItem;
    });

    cardButton.innerHTML += total_count + ")";

    totalPrice.innerHTML = total.toFixed(2);

    document.querySelectorAll(".increase-btn").forEach((btn) => {
        btn.onclick = (e) => {
            console.log("a");
            let id = e.target.getAttribute("data-id");
            let basket = JSON.parse(localStorage.getItem("basket"));
            let foundBasketItem = basket.find((x) => x.id == id);
            foundBasketItem.count++;
            localStorage.setItem("basket", JSON.stringify(basket));
            renderBasketSection();
        };
    });

    document.querySelectorAll(".decrease-btn").forEach((btn) => {
        btn.onclick = (e) => {
            let id = e.target.getAttribute("data-id");
            let basket = JSON.parse(localStorage.getItem("basket"));
            let foundBasketItem = basket.find((x) => x.id == id);
            if (foundBasketItem.count == 0) return;
            foundBasketItem.count--;
            basket = basket.filter((x) => x.count != 0)
            localStorage.setItem("basket", JSON.stringify(basket));
            renderBasketSection();
        };
    });

    document.querySelectorAll(".delete-btn").forEach((btn) => {
        btn.onclick = (e) => {
            let id = e.target.getAttribute("data-id");
            let basket = JSON.parse(localStorage.getItem("basket"));
            basket = basket.filter((x) => x.id != id);
            localStorage.setItem("basket", JSON.stringify(basket));
            renderBasketSection();
        };
    });
}