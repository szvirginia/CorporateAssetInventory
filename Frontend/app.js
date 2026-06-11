const apiUrl = 'http://localhost:5001/api/Assets';

// xss security: escape HTML special characters to prevent injection attacks
function escapeHTML(str) {
    if (!str) return '';
    return str.toString().replace(/[&<>'"]/g,
        tag => ({
            '&': '&amp;',
            '<': '&lt;',
            '>': '&gt;',
            "'": '&#39;',
            '"': '&quot;'
        }[tag] || tag)
    );
}

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
                <td>${escapeHTML(asset.assetName)}</td>
                <td>${escapeHTML(asset.serialNumber)}</td>
                <td>${statusText}</td>
                <td>${escapeHTML(employeeName)}</td>
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
    if (!confirm("❌ Are you sure about deleting this asset?")) {
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
            alert('🎯 Asset added successfully!');
        }
        else if (response.status === 400) {
            const errorData = await response.json();
            console.error('Validation error:', errorData);

            let errorMessages = [];
            const validationErrors = errorData.errors || errorData;

            for (const key in validationErrors) {
                if (validationErrors.hasOwnProperty(key)) {
                    errorMessages.push(`${validationErrors[key].join(', ')}`);
                }
            }
            alert("⚠️ Validation failed:\n\n" + errorMessages.join('\n'));
        }
        else {
            const text = await response.text();
            console.error('Failed to add asset', text);
            alert('❌ Failed to add asset. See console for details.');
        }
    } catch (error) {
        console.error('Failed to add asset', error);
        alert('❌ Failed to add asset. See console for details.');
    }
}

const addStatusSelect = document.getElementById('asset-type');
const addEmployeeSelect = document.getElementById('employee-id');

function checkAddStatus() {
    if (addStatusSelect.value !== "1") {
        addEmployeeSelect.value = "";
        addEmployeeSelect.disabled = true;
        addEmployeeSelect.style.opacity = "0.5";
    } else {
        addEmployeeSelect.disabled = false;
        addEmployeeSelect.style.opacity = "1";
    }
}

addStatusSelect.addEventListener('change', checkAddStatus);
checkAddStatus();

async function openEditModal(id) {
    try {
        const response = await fetch(`${apiUrl}/${id}`);
        const asset = await response.json();

        document.getElementById("edit-id").value = asset.id;
        document.getElementById("edit-asset-name").value = asset.assetName;
        document.getElementById("edit-serial-number").value = asset.serialNumber;
        document.getElementById("edit-status").value = asset.type;

        const empResp = await fetch('http://localhost:5001/api/Employees');
        const employees = await empResp.json();
        const select = document.getElementById("edit-employee-id");
        select.innerHTML = '<option value="">Unassigned</option>';

        employees.forEach(e => {
            const isSelected = asset.employeeId === e.id ? 'selected' : '';
            select.innerHTML += `<option value="${e.id}" ${isSelected}>${escapeHTML(e.name)}</option>`;
        });

        const checkStatus = () => {
            const status = document.getElementById("edit-status").value;
            if (status !== "1") {
                select.value = "";
                select.disabled = true;
                select.style.opacity = "0.5";
            }
            else {
                select.disabled = false;
                select.style.opacity = "1";
            }
        };

        document.getElementById("edit-status").onchange = checkStatus;
        checkStatus();

        document.getElementById("edit-modal").style.display = 'flex';

    } catch (error) {
        console.log("Error opening edit modal:", error);

    }
}

function closeModal() {
    document.getElementById("edit-modal").style.display = 'none';
}
document.getElementById("btn-close-modal").addEventListener('click', closeModal);

document.getElementById("btn-save-edit").addEventListener("click", async () => {
    const id = document.getElementById("edit-id").value;
    const name = document.getElementById("edit-employee-id").value;

    const updatedAsset = {
        id: parseInt(id),
        assetName: document.getElementById("edit-asset-name").value.trim(),
        serialNumber: document.getElementById("edit-serial-number").value.trim(),
        type: parseInt(document.getElementById("edit-status").value),
        employeeId: name === '' ? null : parseInt(name)
    };

    try {
        const response = await fetch(`${apiUrl}/${id}`, {
            method: 'PUT',
            headers: { 'Content-Type': 'application/json' },
            body: JSON.stringify(updatedAsset)
        });

        if (response.ok) {
            closeModal();
            loadAssets();
        } else {
            alert('Failed to update asset. See console for details.');
        }
    } catch (error) {
        console.error("Failed to update asset: ", error);
    }
});