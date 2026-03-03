const API = "http://backend-service:5000/api/orders";

function formatCurrency(amount) {
    return new Intl.NumberFormat("en-IN", {
        style: "currency",
        currency: "INR"
    }).format(amount);
}

document.getElementById("orderForm").addEventListener("submit", async e => {
    e.preventDefault();

    const amountInput = document.getElementById("amount");
    const amount = amountInput.value;

    await fetch(API, {
        method: "POST",
        headers: { "Content-Type": "application/json" },
        body: JSON.stringify({ amount })
    });

    amountInput.value = "";
    loadOrders();
});

async function loadOrders() {
    const res = await fetch(API);
    const data = await res.json();

    const table = document.getElementById("orders");
    table.innerHTML = "";

    data.forEach(o => {
        table.innerHTML += `
            <tr>
                <td>${o.id}</td>
                <td>${new Date(o.orderDate).toLocaleString()}</td>
                <td>${formatCurrency(o.amount)}</td>
            </tr>
        `;
    });
}

loadOrders();

