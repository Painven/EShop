function addProductToCart(product, quantityDirection) {
    const cart = JSON.parse(localStorage.getItem("cart"));

    if (!cart) {
        clearCartProducts();
        cart = JSON.parse(localStorage.getItem("cart"));
    }

    const existingProducts = cart.products.find((p) => p.id == product.id);

    if (existingProducts) {
        existingProducts.quantity += quantityDirection;
    } else if (quantityDirection == 1) {
        cart.products.push({
            id: product.id,
            name: product.name,
            price: product.price,
            quantity: 1,
        });
    }

    const cartNew = JSON.stringify(cart);
    localStorage.setItem("cart", cartNew);

    window.dispatchEvent(
        new CustomEvent("cart-localstorage-changed", (event) => {})
    );
}

function removeProductFromCart(product) {
    const cart = JSON.parse(localStorage.getItem("cart"));
    const existingProducts = cart.products.find((p) => p.id == product.id);

    if (existingProducts) {
        cart.products.splice(
            cart.products.findIndex((p) => p.id == product.id),
            1
        );
    }

    const cartNew = JSON.stringify(cart);
    localStorage.setItem("cart", cartNew);
    window.dispatchEvent(
        new CustomEvent("cart-localstorage-changed", (event) => {})
    );
}

function clearCartProducts() {
    localStorage.setItem("cart", JSON.stringify({ products: [] }));
    window.dispatchEvent(
        new CustomEvent("cart-localstorage-changed", (event) => {})
    );
}
