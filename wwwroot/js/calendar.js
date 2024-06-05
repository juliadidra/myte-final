﻿/*
        document.addEventListener('DOMContentLoaded', function () {
            const quinzenaLabel = document.getElementById('quinzena-label');
            const prevButton = document.getElementById('prev');
            const nextButton = document.getElementById('next');
            const calendarBody = document.getElementById('calendar-body');
            let currentDate = new Date();
            let currentQuinzena = 1;

            const wbsList = @Html.Raw(Json.Serialize(Model.WbsList));
            const registroHorasList = @Html.Raw(Json.Serialize(Model.RegistroHorasList));
            const email = '@ViewBag.Email';

            console.log('WBS List:', wbsList);
            console.log('Registro Horas List:', registroHorasList);

            function getQuinzena(date) {
                const day = date.getDate();
                return day <= 15 ? 1 : 2;
            }

            function parseDate(dateString) {
                if (!dateString) {
                    console.error('Invalid date string:', dateString);
                    return null;
                }
                const [year, month, day] = dateString.split('-');
                return new Date(year, month - 1, day);
            }

            function renderCalendar() {
                const year = currentDate.getFullYear();
                const month = currentDate.getMonth();
                const startDay = currentQuinzena === 1 ? 1 : 16;
                const endDay = currentQuinzena === 1 ? 15 : new Date(year, month + 1, 0).getDate();

                if (quinzenaLabel) {
                    quinzenaLabel.textContent = `Quinzena ${currentQuinzena} de ${month + 1}/${year}`;
                }

                calendarBody.innerHTML = '';

                const dailyTotals = Array(endDay - startDay + 1).fill(0);

                wbsList.forEach(wbs => {
                    if (!wbs || !wbs.nome) {
                        console.error('WBS data is missing:', wbs);
                        return;
                    }

                    console.log('Processing WBS:', wbs.nome);

                    const row = document.createElement('tr');
                    const wbsNameCell = document.createElement('td');
                    wbsNameCell.textContent = wbs.nome;
                    row.appendChild(wbsNameCell);

                    for (let day = startDay; day <= endDay; day++) {
                        console.log('Processing Day:', day);

                        const cell = document.createElement('td');
                        const date = new Date(year, month, day);
                        if (isNaN(date)) {
                            console.error('Invalid date:', date);
                            continue;
                        }
                        const dateString = date.toISOString().split('T')[0];

                        const registro = registroHorasList.find(r => {
                            const registroDate = parseDate(r.dia);

                            console.log('Comparing dates:', registroDate, dateString, r.wbS_Codigo, wbs.codigo, r.id);
                            return registroDate && registroDate.toISOString().split('T')[0] === dateString && r.wbS_Codigo === wbs.codigo;
                        });
                        const registroId = registro ? registro.id : '';
                        console.log(registroId);
                        const horas = registro ? registro.horas : '';
                        console.log('Horas:', horas);

                        if (registro) {
                            dailyTotals[day - startDay] += horas;
                        }

                        cell.innerHTML = `
                <div>Dia ${day}</div>
                <div class="d-flex div_horas_edit">
                <input type="number" value="${horas}" disabled class="input_horas" data-dia="${dateString}" data-wbs="${wbs.codigo}" />
                <a href="/Calendar/UpdateRegistroHoras/${registroId}?email=${email}" class="botao_editar_horas">
                <img src="/assets/icon_edit.svg" class="img_edit_horas">
                </a>
                </div>`;
                        row.appendChild(cell);
                    }
                    calendarBody.appendChild(row);
                });

                // Atualizar totais diários no rodapé
                for (let i = 0; i < dailyTotals.length; i++) {
                    document.getElementById(`total-dia-${i + 1}`).textContent = dailyTotals[i];
                }
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
        */