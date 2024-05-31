document.addEventListener('DOMContentLoaded', () => {
    const quinzenaLabel = document.getElementById('quinzena-label');
    const prevButton = document.getElementById('prev');
    const nextButton = document.getElementById('next');
    const headerRow = document.getElementById('header-row');
    const calendarBody = document.getElementById('calendar-body');
    let currentDate = new Date();
    let currentQuinzena = 1; // 1 para primeira quinzena, 2 para segunda quinzena

    const daysOfWeek = {
        "Sunday": "Dom",
        "Monday": "Seg",
        "Tuesday": "Ter",
        "Wednesday": "Qua",
        "Thursday": "Qui",
        "Friday": "Sex",
        "Saturday": "Sáb"
    };

    function renderCalendar() {
        const year = currentDate.getFullYear();
        const month = currentDate.getMonth() + 1;
        quinzenaLabel.textContent = `Quinzena ${currentQuinzena} - ${month}/${year}`;

        fetch(`/Calendar/GetDays?date=${currentDate.toISOString()}&quinzena=${currentQuinzena}`)
            .then(response => response.json())
            .then(data => {
                headerRow.innerHTML = '<th>WBS</th>';
                data.forEach(day => {
                    const th = document.createElement('th');
                    if (day.dayOfWeek === 'Sunday' || day.dayOfWeek === 'Saturday') {
                        th.classList.add('weekend'); // Adicione a classe 'weekend' aos dias de sábado e domingo
                    }
                    th.innerHTML = `<div>${daysOfWeek[day.dayOfWeek]}</div><div>${day.day}</div>`;
                    headerRow.appendChild(th);
                });

                fetch('/Calendar/GetAllWbs')
                    .then(response => response.json())
                    .then(wbsList => {
                        calendarBody.innerHTML = '';
                        for (let i = 0; i < 7; i++) {
                            const tr = document.createElement('tr');
                            const tdWBS = document.createElement('td');
                            const select = document.createElement('select');
                            select.classList.add('wbs-select');
                            wbsList.forEach(wbsOption => {
                                const option = document.createElement('option');
                                option.value = wbsOption.codigo;
                                option.textContent = wbsOption.nome;
                                select.appendChild(option);
                            });
                            tdWBS.appendChild(select);
                            tr.appendChild(tdWBS);

                            data.forEach(day => {
                                const td = document.createElement('td');
                                const input = document.createElement('input');
                                input.type = 'number';
                                input.classList.add('hours-input');
                                input.setAttribute('data-day', day.day);
                                input.setAttribute('data-dayofweek', day.dayOfWeek);

                                if (day.dayOfWeek === 'Sunday' || day.dayOfWeek === 'Saturday') {
                                    input.disabled = true;
                                    // Adicione a classe 'weekend' aos dias de sábado e domingo
                                    td.classList.add('weekend'); // para a célula do dia                                    
                                }

                                td.appendChild(input);
                                tr.appendChild(td);
                            });

                            calendarBody.appendChild(tr);
                        }
                    });



            });
    }

  


    prevButton.addEventListener('click', () => {
        if (currentQuinzena === 1) {
            currentDate.setMonth(currentDate.getMonth() - 1);
            currentQuinzena = 2;
        } else {
            currentQuinzena = 1;
        }
        renderCalendar();
    });

    nextButton.addEventListener('click', () => {
        if (currentQuinzena === 2) {
            currentDate.setMonth(currentDate.getMonth() + 1);
            currentQuinzena = 1;
        } else {
            currentQuinzena = 2;
        }
        renderCalendar();
    });

    renderCalendar();
});


