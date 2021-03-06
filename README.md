# ProgressEnergyServices
Technical Test


This little package aims to deliver a mail reader program and a user application as a demo.program for ProcessEnergyService.be

Technology used

Entity Framework 6.0 (nuget package)
.Net Framework 4.7.2 (C# 7.xx)
SQL Server Express (ADO.NET DB)
POP3 mail receving (nuget package used MailKit) 
    - used my personal gmail account with application token created for my deskop
    - therefore, it will not work as is from another device (ref:https://github.com/elpro71/ProgressEnergyServices/blob/Add-mail-daemon-and-UI-user-interface/test-energy-provider/HMA_MailAgent/Program.cs line 18)


TODO : 
    - Automatic reply from mail agent
    - UI (WPF MVVM applied (fat client - no communcation adaptor))
    
    - Unit test project