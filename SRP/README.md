# SrpLab — Tricky SRP Violations (C#)

Lab for **Simulation** diploma students.

This repo contains **10 C# classes**. Each one **violates the Single Responsibility Principle**, but not in the cartoon “god class with 3 labeled method groups” way.

Expect:

- Concerns that *look* related (same domain story)
- Multiple **reasons to change** tangled in one type
- Side effects hiding inside “simple” methods
- Formatting / export / messaging mixed with rules

## Projects

| Project | Role |
|---------|------|
| `src/SrpLab` | The 10 classes |
| `src/SrpLab.Runner` | Smoke-runs each class |

## Classes

1. `WardBoard`
2. `CheckoutBasket`
3. `SupportTicket`
4. `LoanDesk`
5. `CourseEnrollmentDesk`
6. `KitchenTicket`
7. `SubscriptionBilling`
8. `WarehousePickList`
9. `GradeBook`
10. `AppointmentDesk`

## Run

```bash
dotnet run --project src/SrpLab.Runner
```

## Your task

For **each** class:

1. Name the distinct responsibilities / reasons to change
2. Refactor into cohesive types (still **no interfaces required** unless your instructor asks)
3. Keep behavior equivalent — runner output should stay meaningful

There is **no solution branch** on purpose.
