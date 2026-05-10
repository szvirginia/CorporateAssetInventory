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
                <td>
                 <button onclick="openEditModal(${asset.id})" class="table-btn" style="background-color: #ffc107;">Edit</button>
                 <button onclick="deleteAsset(${asset.id})" class="table-btn" style="background-color: #dc3545; color: white;">Delete</button>
                </td>
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

const searchInput = document.getElementById("search-input");

searchInput.addEventListener("input", function (event) {
    const searchTerm = event.target.value.toLowerCase();
    const tableRows = document.querySelectorAll("#table-body tr");

    tableRows.forEach(row => {
        const rowText = row.textContent.toLowerCase();
        if (rowText.includes(searchTerm)) {
            row.style.display = "";
        }
        else {
            row.style.display = "none";
        }
    });
});

async function deleteAsset(id) {
    if (!confirm("Are you sure about deleting this asset?")) {
        return;
    }

    try {
        const response = await fetch(`${apiUrl}/${id}`, {
            method: 'DELETE'
        });

        if (response.ok) {
            loadAssets();
        }
        else {
            console.error("Delete method denied.")
        }
    }
    catch (error) {
        console.error("Failed to delete: ", error);
    }

}

// Add Asset handler
document.getElementById('btn-add').addEventListener('click', createAsset);

async function createAsset() {
    const name = document.getElementById('asset-name').value.trim();
    const serial = document.getElementById('serial-number').value.trim();
    const type = parseInt(document.getElementById('asset-type').value);
    const empVal = document.getElementById('employee-id').value;
    const employeeId = empVal === '' ? null : parseInt(empVal);

    if (!name || !serial) {
        alert('Please provide asset name and serial number.');
        return;
    }

    const payload = { assetName: name, serialNumber: serial, type: type, employeeId: employeeId };

    try {
        const response = await fetch(apiUrl, {
            method: 'POST',
            headers: { 'Content-Type': 'application/json' },
            body: JSON.stringify(payload)
        });

        if (response.ok) {
            document.getElementById('asset-name').value = '';
            document.getElementById('serial-number').value = '';
            document.getElementById('asset-type').value = '0';
            document.getElementById('employee-id').value = '';
            loadAssets();
        } else {
            const text = await response.text();
            console.error('Failed to add asset', text);
            alert('Failed to add asset. See console for details.');
        }
    } catch (error) {
        console.error('Failed to add asset', error);
        alert('Failed to add asset. See console for details.');
    }
}