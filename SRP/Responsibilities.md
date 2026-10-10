# SRP Responsibilities

This document identifies the distinct responsibilities found in the ten classes before any refactoring is performed. Each responsibility represents a separate reason the class may need to change.

---

## 1. WardBoard

### Responsibilities

1. **Bed assignment and patient tracking**  
   Assigns patients to beds and keeps track of which patient is associated with each bed.

2. **Clinical acuity calculation**  
   Calculates an acuity score from the patient's heart rate and SpO2 values.

3. **Pager alerting**  
   Decides when a `CODE-YELLOW` alert should be generated and stores the pager log.

4. **Nurse handoff note generation**  
   Builds a human-readable handoff note containing the bed, patient, acuity, and status.

5. **Census CSV export**  
   Converts the ward information into CSV text for external use.

### Why is this a problem?

These responsibilities have different reasons to change. Bed and patient management may change because of ward workflow requirements, while clinical scoring may change because of medical policy. Pager rules can change independently from handoff wording, and the CSV format may change because of an external reporting requirement. Keeping all of these concerns in one class means a change in one area can force us to modify the same class that contains unrelated behavior.

---

## 2. CheckoutBasket

### Responsibilities

1. **Basket and product-line management**  
   Adds products to the basket and stores their SKU, price, and quantity.

2. **Pricing and total calculation**  
   Calculates the subtotal, discounts, gift-wrap fee, and final total.

3. **Coupon parsing and discount rules**  
   Interprets coupon text such as `SAVE20`, `WELCOME10`, and `FREESHIP` and decides the applicable discount.

4. **Gift-message generation**  
   Produces customer-facing text for a gift message card.

5. **Payment authorization**  
   Creates the payment authorization stub/code.

### Why is this a problem?

These concerns can change independently. Pricing rules may change without changing gift-message wording, while marketing may introduce new coupon formats without changing basket management. Payment authorization can also change because of a payment-provider or security requirement. Putting all of these concerns together makes the basket class responsible for unrelated business and communication rules.

---

## 3. SupportTicket

### Responsibilities

1. **Ticket content management**  
   Stores the ticket information and appends new customer messages.

2. **Priority classification**  
   Determines the ticket priority by scanning the subject and body for keywords.

3. **SLA calculation and breach checking**  
   Determines the SLA deadline based on priority and checks whether the SLA has been breached.

4. **Public customer-reply generation**  
   Creates the message sent to the customer.

5. **Internal escalation-message generation**  
   Creates internal escalation text containing priority and SLA information.

### Why is this a problem?

Each concern can change for a different reason. Priority keywords may change as the support team changes its classification rules, while SLA durations may change because of service-level policy. Customer-facing wording and internal escalation wording can also change independently. Keeping them together means the ticket class must change whenever any of these separate policies or message formats changes.

---

## 4. LoanDesk

### Responsibilities

1. **Loan risk assessment and eligibility**  
   Calculates the risk score and determines whether the applicant is eligible.

2. **Required-document rules**  
   Determines which documents the applicant must provide based on the loan request and applicant information.

3. **Decision-letter generation**  
   Creates the approval or rejection letter shown to the applicant.

4. **Underwriter CSV export**  
   Converts the loan information and decision into a CSV row for the underwriter.

### Why is this a problem?

Risk and eligibility rules may change because of lending policy, while required documents may change because of compliance requirements. The wording of decision letters belongs to communication and may change independently. The CSV format is an external reporting/integration concern and may change without any change to the loan decision rules. These independent reasons for change should not all be tied to the same class.

---

## 5. CourseEnrollmentDesk

### Responsibilities

1. **Course enrollment and seat management**  
   Registers students, manages available seats, prevents duplicate registration, and tracks seated students.

2. **Waitlist management**  
   Maintains the waitlist, calculates positions, and promotes students when seats become available.

3. **Welcome-packet generation**  
   Creates the Markdown welcome message containing the student's course status and onboarding information.

4. **Tuition invoice generation**  
   Calculates VAT and produces the tuition invoice line.

### Why is this a problem?

Enrollment and waitlist rules belong to course administration, while the welcome packet is customer/student communication. Tuition and VAT rules belong to financial processing and can change because of finance or tax requirements. These concerns have different reasons to change, so keeping them together makes changes in one area affect a class that also contains unrelated enrollment and communication logic.

---

## 6. KitchenTicket

### Responsibilities

1. **Kitchen-item management**  
   Stores ordered items, their ingredients, and preparation times.

2. **Allergen detection**  
   Scans ingredients and identifies allergens such as dairy, gluten, nuts, and shellfish.

3. **Preparation-time estimation**  
   Estimates when the order will be ready based on preparation times and available stations.

4. **Thermal-ticket rendering**  
   Formats the order as a printable kitchen ticket.

5. **Expo-lane routing**  
   Determines whether the order should use the allergy, slow, or fast lane.

### Why is this a problem?

These responsibilities have different reasons to change. Allergen rules may change because of food-safety requirements, while preparation-time calculations may change because of kitchen operations. The printed ticket layout can change because of printer or UX requirements, and expo-lane rules can change because of kitchen workflow. Keeping them together makes unrelated operational and presentation rules depend on the same class.

---

## 7. SubscriptionBilling

### Responsibilities

1. **Subscription billing and proration calculation**  
   Calculates the amount to charge based on the subscription period and activation date.

2. **Invoice-number generation**  
   Generates sequential invoice numbers.

3. **Failed-payment tracking and dunning policy**  
   Tracks failed payments and determines the severity of the collection notice.

4. **Dunning-email generation**  
   Creates the customer-facing payment reminder email.

5. **Ledger journal export**  
   Produces the accounting ledger line containing subscription and invoice information.

### Why is this a problem?

Billing calculations, invoice numbering, collections policy, customer communication, and accounting export are separate concerns. For example, changing the invoice-numbering scheme should not require changing the billing calculation. Likewise, changing the accounting format should not affect the customer email. Combining them also creates unnecessary coupling; for example, generating a dunning email currently generates a new invoice number as a side effect.

---

## 8. WarehousePickList

### Responsibilities

1. **Pick-list need management**  
   Stores the SKU, aisle, bin, required quantity, and available quantity for each item.

2. **Stock allocation**  
   Determines how much of each requested item can actually be allocated from available stock.

3. **Warehouse walking-order calculation**  
   Determines the order in which the picker should visit aisles and bins.

4. **Picker instruction generation**  
   Creates human-readable instructions for the warehouse picker and reports shortages.

5. **WMS XML export**  
   Converts the allocated items into the XML format required by the warehouse management system.

### Why is this a problem?

Stock allocation, warehouse routing, human-readable instructions, and WMS integration can all change independently. A new warehouse layout may require a different walking strategy without changing stock allocation. The handheld-device wording may change without changing either of those rules, while the WMS XML contract may change because of an external integration. Keeping all of these concerns together gives one class several unrelated reasons to change.

---

## 9. GradeBook

### Responsibilities

1. **Grade storage and recording**  
   Stores student scores and records new grades.

2. **Academic evaluation policy**  
   Calculates averages, assigns letter grades, and determines honor-roll eligibility.

3. **Transcript generation**  
   Produces a human-readable transcript.

4. **CSV export**  
   Converts grade information into CSV format.

### Why is this a problem?

Grade storage and academic evaluation are related to the same domain but still have different concerns from document and export formatting. Academic policies may change when grading rules change, while transcript formatting may change because of registrar requirements. CSV structure may change for another external consumer without any change to the grading policy. Therefore, all of these responsibilities do not need to be owned by the same class.

---

## 10. AppointmentDesk

### Responsibilities

1. **Appointment scheduling and booking**  
   Manages booked slots, business hours, slot alignment, searching for available slots, and booking appointments.

2. **ICS calendar serialization**  
   Converts an appointment into an ICS calendar event.

3. **SMS reminder generation**  
   Creates the text of the appointment reminder message.

### Why is this a problem?

Appointment scheduling, calendar serialization, and messaging have different reasons to change. Clinic hours or booking rules may change without any need to change the ICS format. Similarly, the SMS wording or communication channel may change without changing appointment scheduling. Keeping these concerns in one class couples scheduling logic with unrelated integration and communication formats.

---

## Conclusion

The main SRP violation across these classes is not simply that they contain many methods. The problem is that each class contains multiple responsibilities with different reasons to change.

The responsibilities should be separated according to the different policies, business rules, presentation formats, or external integrations that can change independently. This analysis identifies those responsibilities before any refactoring is performed.