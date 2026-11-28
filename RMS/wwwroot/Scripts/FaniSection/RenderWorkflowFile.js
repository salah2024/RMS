
function RenderWorkflow(data) {

    if (!data || !data.steps || data.steps.length === 0) {
        $('#workflowSchimaTitle').html('');
        $("#workflowViewer").html(
            '<div class="alert alert-info text-center">گردش کاری برای این پروژه ثبت نشده است.</div>'
        );
        $('#aworkflowSchimaShow').click();
        return;
    }

    // --------------------------------
    // ابزارهای کمکی
    // --------------------------------

    const escapeHtml = value => {
        return String(value ?? '').replace(/[&<>"']/g, char => ({
            '&': '&amp;',
            '<': '&lt;',
            '>': '&gt;',
            '"': '&quot;',
            "'": '&#39;'
        })[char]);
    };

    const formatDate = value => {
        if (!value) return '—';

        const date = new Date(value);

        if (isNaN(date.getTime()))
            return escapeHtml(value);

        return date.toLocaleString('fa-IR-u-ca-persian', {
            year: 'numeric',
            month: '2-digit',
            day: '2-digit',
            hour: '2-digit',
            minute: '2-digit',
            hourCycle: 'h23'
        });
    };

    const actionTitleFa = title => {
        const map = {
            'Approve': 'تأیید',
            'Approved': 'تأیید شده',
            'Submit': 'ارسال',
            'Submitted': 'ارسال شده',
            'RevisionRequested': 'درخواست اصلاح',
            'Reject': 'رد',
            'Rejected': 'رد شده',
            'Return': 'عودت',
            'Cancelled': 'لغو شده',
            'Cancel': 'لغو',
            'Start': 'شروع گردش کار'
        };

        return map[title] || title || 'اقدام ثبت شده';
    };

    const statusInfo = title => {

        const map = {
            'تأیید شده': {
                css: 'completed',
                icon: 'fa-check-circle'
            },
            'اقدامات تکمیل شده': {
                css: 'completed',
                icon: 'fa-check-circle'
            },
            'رد شده': {
                css: 'rejected',
                icon: 'fa-times-circle'
            },
            'درخواست اصلاح': {
                css: 'revision',
                icon: 'fa-refresh'
            },
            'در انتظار اقدام': {
                css: 'pending',
                icon: 'fa-clock-o'
            },
            'شروع نشده': {
                css: 'not-started',
                icon: 'fa-circle-o'
            }
        };

        return map[title] || {
            css: 'pending',
            icon: 'fa-clock-o'
        };
    };

    const assignmentInfo = user => {

        switch (user.result) {

            case 1:
                return {
                    title: 'در انتظار اقدام',
                    css: 'pending',
                    icon: 'fa-clock-o'
                };

            case 2:
                return {
                    title: 'ارسال شده',
                    css: 'submitted',
                    icon: 'fa-paper-plane'
                };

            case 3:
                return {
                    title: 'درخواست اصلاح',
                    css: 'revision',
                    icon: 'fa-refresh'
                };

            case 4:
                return {
                    title: 'رد شده',
                    css: 'rejected',
                    icon: 'fa-times-circle'
                };

            case 5:
                return {
                    title: 'لغو شده',
                    css: 'cancelled',
                    icon: 'fa-ban'
                };

            case 6:
                return {
                    title: 'تأیید شده',
                    css: 'completed',
                    icon: 'fa-check-circle'
                };

            default:
                return {
                    title: user.resultTitle || 'نامشخص',
                    css: 'pending',
                    icon: 'fa-clock-o'
                };
        }
    };

    // --------------------------------
    // استایل شماتیک
    // --------------------------------

    if (!document.getElementById('workflow-timeline-style')) {

        const style = document.createElement('style');

        style.id = 'workflow-timeline-style';

        style.textContent = `
            #workflowViewer {
                direction: rtl;
                font-family: IRANSans, Tahoma, sans-serif;
                color: #344054;
            }

            #workflowViewer * {
                box-sizing: border-box;
            }

            #workflowViewer .workflow-container {
                width: 100%;
                padding: 14px;
            }

            #workflowViewer .workflow-progress {
                background: #fff;
                border: 1px solid #e8edf3;
                border-radius: 14px;
                padding: 18px 20px;
                margin-bottom: 22px;
                box-shadow: 0 3px 12px rgba(16, 24, 40, .04);
            }

            #workflowViewer .progress-info {
                display: flex;
                justify-content: space-between;
                align-items: center;
                margin-bottom: 12px;
                font-size: 13px;
                font-weight: 600;
            }

            #workflowViewer .progress-percent {
                color: #2563eb;
                font-size: 15px;
                font-weight: 800;
            }

            #workflowViewer .progress-track {
                height: 9px;
                background: #edf1f6;
                border-radius: 20px;
                overflow: hidden;
            }

            #workflowViewer .progress-fill {
                height: 100%;
                background: linear-gradient(90deg, #60a5fa, #2563eb);
                border-radius: 20px;
                transition: width .4s ease;
            }

            #workflowViewer .workflow-flow {
                display: grid;
                grid-template-columns: repeat(auto-fit, minmax(290px, 1fr));
                align-items: stretch;
                gap: 18px;
            }

            #workflowViewer .workflow-step {
                min-width: 0;
                display: flex;
            }

            #workflowViewer .step-card {
                width: 100%;
                min-height: 390px;
                display: flex;
                flex-direction: column;
                background: #fff;
                border: 1px solid #e6eaf0;
                border-radius: 15px;
                padding: 17px;
                box-shadow: 0 3px 12px rgba(16, 24, 40, .045);
                transition: box-shadow .2s ease, transform .2s ease;
            }

            #workflowViewer .step-card:hover {
                box-shadow: 0 7px 20px rgba(16, 24, 40, .09);
                transform: translateY(-2px);
            }

            #workflowViewer .workflow-step.current .step-card {
                border: 1.5px solid #4b8de8;
                background: #fbfdff;
                box-shadow: 0 4px 17px rgba(37, 99, 235, .10);
            }

            #workflowViewer .step-header {
                display: flex;
                justify-content: space-between;
                align-items: center;
                gap: 8px;
                margin-bottom: 17px;
                padding-bottom: 12px;
                border-bottom: 1px solid #edf0f4;
            }

            #workflowViewer .step-number {
                font-size: 12px;
                font-weight: 700;
                color: #667085;
                background: #f2f4f7;
                padding: 6px 11px;
                border-radius: 20px;
            }

            #workflowViewer .current-label {
                font-size: 11px;
                color: #1d64c8;
                background: #eaf3ff;
                padding: 5px 10px;
                border-radius: 20px;
                font-weight: 700;
            }

            #workflowViewer .step-title {
                display: flex;
                align-items: center;
                gap: 10px;
                min-height: 42px;
                margin-bottom: 12px;
            }

            #workflowViewer .step-title h4 {
                font-size: 15px;
                font-weight: 800;
                margin: 0;
                color: #253247;
                line-height: 1.8;
            }

            #workflowViewer .step-main-icon {
                width: 35px;
                height: 35px;
                flex: 0 0 35px;
                display: flex;
                align-items: center;
                justify-content: center;
                border-radius: 10px;
                font-size: 16px;
            }

            #workflowViewer .step-main-icon.completed {
                background: #e8f7ef;
                color: #16834a;
            }

            #workflowViewer .step-main-icon.pending {
                background: #fff4df;
                color: #bd7b13;
            }

            #workflowViewer .step-main-icon.rejected {
                background: #fdecec;
                color: #c43b3b;
            }

            #workflowViewer .step-main-icon.revision {
                background: #f3edff;
                color: #8053c7;
            }

            #workflowViewer .step-main-icon.not-started {
                background: #f2f4f7;
                color: #98a2b3;
            }

            #workflowViewer .step-status {
                display: inline-flex;
                align-items: center;
                gap: 6px;
                align-self: flex-start;
                font-size: 12px;
                font-weight: 700;
                padding: 6px 11px;
                border-radius: 20px;
                margin-bottom: 17px;
            }

            #workflowViewer .step-status.completed,
            #workflowViewer .user-status.completed {
                color: #16834a;
                background: #e8f7ef;
            }

            #workflowViewer .step-status.pending,
            #workflowViewer .user-status.pending {
                color: #a56a0b;
                background: #fff4df;
            }

            #workflowViewer .step-status.rejected,
            #workflowViewer .user-status.rejected {
                color: #c43b3b;
                background: #fdecec;
            }

            #workflowViewer .step-status.revision,
            #workflowViewer .user-status.revision {
                color: #8053c7;
                background: #f3edff;
            }

            #workflowViewer .step-status.not-started {
                color: #667085;
                background: #f2f4f7;
            }

            #workflowViewer .step-status.submitted,
            #workflowViewer .user-status.submitted {
                color: #2466b3;
                background: #eaf3ff;
            }

            #workflowViewer .step-status.cancelled,
            #workflowViewer .user-status.cancelled {
                color: #667085;
                background: #eef0f3;
            }

            #workflowViewer .section-title {
                font-size: 12px;
                font-weight: 800;
                color: #475467;
                padding-bottom: 10px;
                margin-bottom: 10px;
                border-bottom: 1px solid #edf0f4;
            }

            #workflowViewer .assignment-list {
                margin-bottom: 15px;
            }

            #workflowViewer .assignment-item {
                background: #f9fafb;
                border: 1px solid #edf0f4;
                border-radius: 11px;
                padding: 11px;
                margin-bottom: 9px;
            }

            #workflowViewer .assignment-user {
                display: flex;
                align-items: center;
                gap: 9px;
                flex-wrap: wrap;
            }

            #workflowViewer .user-icon {
                width: 34px;
                height: 34px;
                flex: 0 0 34px;
                display: flex;
                align-items: center;
                justify-content: center;
                background: #edf3fb;
                color: #3974b9;
                border-radius: 50%;
            }

            #workflowViewer .user-info {
                flex: 1;
                min-width: 100px;
            }

            #workflowViewer .user-name {
                font-size: 13px;
                font-weight: 800;
                color: #344054;
                line-height: 1.8;
            }

            #workflowViewer .company {
                font-size: 11px;
                color: #7b8493;
                margin-top: 2px;
            }

            #workflowViewer .user-status {
                font-size: 10px;
                font-weight: 700;
                padding: 5px 8px;
                border-radius: 15px;
                white-space: nowrap;
            }

            #workflowViewer .assignment-meta {
                display: flex;
                flex-wrap: wrap;
                gap: 7px 14px;
                margin-top: 10px;
                padding-top: 8px;
                border-top: 1px dashed #e1e6ed;
                color: #667085;
                font-size: 10px;
                line-height: 1.9;
            }

            #workflowViewer .assignment-comment,
            #workflowViewer .history-comment {
                background: #fff9e9;
                color: #8a671c;
                border-radius: 8px;
                padding: 8px 10px;
                font-size: 11px;
                line-height: 1.9;
                margin-top: 9px;
                overflow-wrap: anywhere;
            }

            #workflowViewer .history-list {
                margin-top: 10px;
            }

            #workflowViewer .history-item {
                display: flex;
                gap: 10px;
                padding: 10px;
                background: #fafbfc;
                border: 1px solid #edf0f4;
                border-radius: 10px;
                margin-bottom: 8px;
            }

            #workflowViewer .history-action {
                width: 32px;
                height: 32px;
                flex: 0 0 32px;
                display: flex;
                justify-content: center;
                align-items: center;
                background: #eaf3ff;
                color: #2466b3;
                border-radius: 50%;
            }

            #workflowViewer .history-content {
                min-width: 0;
                flex: 1;
            }

            #workflowViewer .history-user {
                font-size: 12px;
                font-weight: 800;
                color: #344054;
            }

            #workflowViewer .history-action-title {
                font-size: 11px;
                color: #475467;
                margin-top: 4px;
            }

            #workflowViewer .history-date {
                font-size: 10px;
                color: #7b8493;
                margin-top: 5px;
                direction: rtl;
                font-variant-numeric: tabular-nums;
            }

            #workflowViewer .no-assignment {
                color: #98a2b3;
                font-size: 11px;
                padding: 12px;
                text-align: center;
                background: #fafbfc;
                border-radius: 9px;
            }

            #workflowViewer .step-footer {
                margin-top: auto;
                padding-top: 12px;
            }

            @media(max-width: 600px) {
                #workflowViewer .workflow-container {
                    padding: 5px;
                }

                #workflowViewer .workflow-flow {
                    grid-template-columns: minmax(0, 1fr);
                }

                #workflowViewer .step-card {
                    min-height: 0;
                }
            }
        `;

        document.head.appendChild(style);
    }

    // --------------------------------
    // محاسبه پیشرفت
    // --------------------------------

    const totalSteps = data.steps.length;

    const completedSteps = data.steps.filter(step =>
        step.assignments &&
        step.assignments.length > 0 &&
        step.assignments.every(a => a.isCompleted)
    ).length;

    const percent = Math.round((completedSteps / totalSteps) * 100);

    $('#workflowSchimaTitle').text(data.projectName || '');

    let html = `
        <div class="workflow-container">

            <div class="workflow-progress">

                <div class="progress-info">
                    <span>میزان پیشرفت گردش کار</span>
                    <span class="progress-percent">
                        ${percent.toLocaleString('fa-IR')}٪
                    </span>
                </div>

                <div class="progress-track">
                    <div class="progress-fill" style="width:${percent}%"></div>
                </div>

            </div>

            <div class="workflow-flow">
    `;

    // --------------------------------
    // ساخت کارت مراحل
    // --------------------------------

    data.steps.forEach((step, index) => {

        const assignments = step.assignments || [];
        const histories = step.histories || [];
        const potentialReceivers = step.potentialReceivers || [];

        const status = statusInfo(step.statusTitle);

        html += `
            <div class="workflow-step ${step.isCurrent ? 'current' : ''}">

                <div class="step-card">

                    <div class="step-header">

                        <span class="step-number">
                            مرحله ${Number(step.order).toLocaleString('fa-IR')}
                        </span>

                        ${step.isCurrent
                            ? '<span class="current-label">مرحله جاری</span>'
                            : ''
                        }

                    </div>

                    <div class="step-title">

                        <span class="step-main-icon ${status.css}">
                            <i class="fa ${status.icon}"></i>
                        </span>

                        <h4>${escapeHtml(step.stepTitle)}</h4>

                    </div>

                    <div class="step-status ${status.css}">
                        ${escapeHtml(step.statusTitle)}
                    </div>
        `;

        // --------------------------------
        // گیرندگان واقعی
        // --------------------------------

        if (assignments.length > 0) {

            html += `
                <div class="assignment-list">

                    <div class="section-title">
                        <i class="fa fa-users"></i>
                        گیرندگان مرحله
                    </div>
            `;

            assignments.forEach(user => {

                const info = assignmentInfo(user);

                let userCss = info.css;
                let userTitle = info.title;
                let userIcon = info.icon;

                if (
                    !user.isCompleted &&
                    (user.result == null || user.result === 1)
                ) {
                    userCss = 'pending';
                    userTitle = 'در انتظار اقدام';
                    userIcon = 'fa-clock-o';
                }

                html += `
                    <div class="assignment-item">

                        <div class="assignment-user">

                            <div class="user-icon">
                                <i class="fa fa-user"></i>
                            </div>

                            <div class="user-info">

                                <div class="user-name">
                                    ${escapeHtml(user.fullName)}
                                </div>

                                <div class="company">
                                    ${escapeHtml(user.companyName || '')}
                                </div>

                            </div>

                            <span class="user-status ${userCss}">
                                <i class="fa ${userIcon}"></i>
                                ${userTitle}
                            </span>

                        </div>

                        <div class="assignment-meta">

                            ${user.assignDate ? `
                                <span>
                                    <i class="fa fa-calendar"></i>
                                    ارجاع: ${formatDate(user.assignDate)}
                                </span>
                            ` : ''}

                            ${user.completedDate ? `
                                <span>
                                    <i class="fa fa-check"></i>
                                    اقدام: ${formatDate(user.completedDate)}
                                </span>
                            ` : ''}

                        </div>

                        ${user.comment ? `
                            <div class="assignment-comment">
                                <i class="fa fa-commenting-o"></i>
                                ${escapeHtml(user.comment)}
                            </div>
                        ` : ''}

                    </div>
                `;
            });

            html += `</div>`;
        }

        // --------------------------------
        // افراد پیشنهادی برای ارجاع
        // --------------------------------

        else if (potentialReceivers.length > 0) {

            html += `
                <div class="assignment-list">

                    <div class="section-title">
                        <i class="fa fa-users"></i>
                        افراد تعیین‌شده برای ارجاع
                    </div>
            `;

            potentialReceivers.forEach(user => {

                html += `
                    <div class="assignment-item">

                        <div class="assignment-user">

                            <div class="user-icon">
                                <i class="fa fa-user"></i>
                            </div>

                            <div class="user-info">

                                <div class="user-name">
                                    ${escapeHtml(user.fullName)}
                                </div>

                                <div class="company">
                                    ${escapeHtml(user.companyName || '')}
                                </div>

                            </div>

                            <span class="user-status pending">
                                در انتظار ارجاع
                            </span>

                        </div>

                    </div>
                `;
            });

            html += `</div>`;
        }

        else if (!step.isCurrent) {

            html += `
                <div class="no-assignment">
                    هنوز ارجاعی برای این مرحله ثبت نشده است.
                </div>
            `;
        }

        // --------------------------------
        // تاریخچه اقدامات
        // --------------------------------

        if (histories.length > 0) {

            html += `
                <div class="history-list">

                    <div class="section-title">
                        <i class="fa fa-history"></i>
                        تاریخچه اقدامات
                    </div>
            `;

            histories.forEach(history => {

                html += `
                    <div class="history-item">

                        <div class="history-action">
                            <i class="fa fa-history"></i>
                        </div>

                        <div class="history-content">

                            <div class="history-user">
                                ${escapeHtml(history.fullName)}
                            </div>

                            <div class="history-action-title">
                                ${escapeHtml(actionTitleFa(history.actionTitle))}
                            </div>

                            <div class="history-date">
                                <i class="fa fa-calendar"></i>
                                ${history.actionDateShamsi
                                    ? escapeHtml(history.actionDateShamsi)
                                    : formatDate(history.actionDate)
                                }
                            </div>

                            ${history.comment ? `
                                <div class="history-comment">
                                    ${escapeHtml(history.comment)}
                                </div>
                            ` : ''}

                        </div>

                    </div>
                `;
            });

            html += `</div>`;
        }

        html += `
                    <div class="step-footer"></div>

                </div>
            </div>
        `;
    });

    html += `
            </div>
        </div>
    `;

    $('#workflowViewer').html(html);

    $('#aworkflowSchimaShow').click();
}
