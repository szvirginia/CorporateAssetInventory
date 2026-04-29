const apiUrl = 'http://localhost:5001/api/Assets';

async function loadAssets() {
    try {
        const response = await fetch(apiUrl);
        if (!response.ok) {
            throw new Error(`HTTP error! status: ${response.status}`);
        }

        const assets = await response.json();

        const tableBody = document.getElementById('table-body');
        tableBody.innerHTML = ' ';

        assets.forEach(asset => {
            let statusText = "";
            if (asset.type == 0) {
                statusText = "In Stock";
            }
            else if (asset.type == 1) {
                statusText = "Assigned";
            }
            else if (asset.type == 2) {
                statusText = "In Repair";
            }

            let employeeName = asset.employee ? asset.employee.name : "Unassigned";

            const tr = document.createElement('tr');
            tr.innerHTML = `
                <td>${asset.assetName}</td>
                <td>${asset.serialNumber}</td>
                <td>${statusText}</td>
                <td>${employeeName}</td>
                <td><button style="padding: 5px 10px; font-size: 12px; background-color: #dc3545;">Delete</button></td>
            `;

            tableBody.appendChild(tr);
        });
    } catch (error) {
        console.error("Failed to load assets:", error)
    }
}

async function loadEmployees() {
    try {
        const response = await fetch('http://localhost:5001/api/Employees');
        if (!response.ok) {
            throw new Error(`HTTP error! status: ${response.status}`);
        }

        const employees = await response.json();

        const employeeSelect = document.getElementById('employee-id');
        employeeSelect.innerHTML = '<option value="">Unassigned</option>';

        employees.forEach(employee => {
            const option = document.createElement('option');
            option.value = employee.id;
            option.textContent = employee.name;
            employeeSelect.appendChild(option);
        });

    } catch (error) {
        console.error("Failed to load employees:", error)
    }
}

loadAssets();
loadEmployees();
