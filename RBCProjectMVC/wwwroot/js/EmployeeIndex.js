
$(document).ready(function () {
    let debounceTimer;
    const debounceDelay = 300;

    const $searchInput = $('#searchInput');
    const $employeeTableBody = $('#employeeTableBody');
    const $totalCount = $('#totalCount');

    let departmentChart; 

   
    fetchEmployees();

    
    $searchInput.on('keyup', function () {
        clearTimeout(debounceTimer);
        const term = $(this).val();
        debounceTimer = setTimeout(() => fetchEmployees(term), debounceDelay);
    });

    
    $('.sort-header').on('click', function (e) {
        e.preventDefault();
        const column = $(this).data('column');
        const currentDir = $(this).data('sortDir') || 'asc';
        const newDir = currentDir === 'asc' ? 'desc' : 'asc';
        $(this).data('sortDir', newDir);

        fetchEmployees($searchInput.val(), column, newDir);
    });

    function fetchEmployees(search = '', sortBy = '', sortDir = 'asc') {
        let url = '/api/employeesapi?';
        if (search) url += `search=${encodeURIComponent(search)}&`;
        if (sortBy) url += `sortBy=${encodeURIComponent(sortBy)}&sortDir=${encodeURIComponent(sortDir)}`;

        $.ajax({
            url: url,
            type: 'GET',
            success: function (data) {
                renderTable(data);
                updateChart(data);
                $totalCount.text(data.length);
            },
            error: function () {
                console.error('API request failed');
            }
        });
    }

    function renderTable(employees) {
        $employeeTableBody.empty();
        employees.forEach(emp => {
            let imgHtml = '';
            if (emp.fileBlob) {
                const byteCharacters = atob(emp.fileBlob.replace(/\r?\n|\r/g, ''));
                const byteNumbers = new Array(byteCharacters.length);
                for (let i = 0; i < byteCharacters.length; i++) {
                    byteNumbers[i] = byteCharacters.charCodeAt(i);
                }
                const byteArray = new Uint8Array(byteNumbers);
                const blob = new Blob([byteArray], { type: 'image/png' });
                const imageUrl = URL.createObjectURL(blob);
                imgHtml = `<img src="${imageUrl}" width="50" />`;
            }

            const row = `
                <tr>
                    <td>${emp.fullName}</td>
                    <td>${emp.position}</td>
                    <td>${emp.department}</td>
                    <td>${new Date(emp.hireDate).toLocaleDateString()}</td>
                    <td>${emp.email}</td>
                    <td>${emp.phone}</td>
                    <td>${emp.salary.toFixed(2)}</td>
                    <td>${imgHtml}</td>
                    <td>
                        <a href="/Employee/Edit/${emp.employeId}" class="btn btn-sm btn-warning">Edit</a>
                        <form action="/Employee/Delete/${emp.employeId}" method="post" class="d-inline">
                            <button type="submit" class="btn btn-sm btn-danger" onclick="return confirm('Are you sure?')">Delete</button>
                        </form>
                    </td>
                </tr>
            `;
            $employeeTableBody.append(row);
        });
    }

    function updateChart(employees) {
        const deptCounts = {};
        employees.forEach(emp => {
            if (emp.department) {
                deptCounts[emp.department] = (deptCounts[emp.department] || 0) + 1;
            }
        });

        const labels = Object.keys(deptCounts);
        const data = Object.values(deptCounts);

        const ctx = document.getElementById('departmentChart').getContext('2d');

        if (departmentChart) {
            departmentChart.data.labels = labels;
            departmentChart.data.datasets[0].data = data;
            departmentChart.update();
        } else {
            departmentChart = new Chart(ctx, {
                type: 'bar',
                data: {
                    labels: labels,
                    datasets: [{
                        label: 'Number of Employees',
                        data: data,
                        backgroundColor: 'rgba(54, 162, 235, 0.6)',
                        borderColor: 'rgba(54, 162, 235, 1)',
                        borderWidth: 1
                    }]
                },
                options: {
                    responsive: true,
                    scales: {
                        y: {
                            beginAtZero: true,
                            precision: 0
                        }
                    }
                }
            });
        }
    }
});
