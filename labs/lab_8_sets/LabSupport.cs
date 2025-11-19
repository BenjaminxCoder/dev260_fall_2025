using System;
using System.Collections.Generic;
using System.Linq;

namespace Lab8_Sets
{
    /// <summary>
    /// Supporting class for Lab 8 Set Operations
    /// Contains menu handlers, UI logic, and demo data setup functions
    /// 
    /// INSTRUCTOR NOTE: This class demonstrates several key software engineering principles:
    /// 
    /// 1. Separation of Concerns: UI logic is completely separate from business logic
    /// 2. Static Utility Pattern: All methods are static since this is pure infrastructure
    /// 3. Delegation Pattern: This class delegates to the SetOperationsLab for actual work
    /// 4. User Experience Focus: Comprehensive error handling and clear feedback
    /// 
    /// Study this code to understand how a professional console application
    /// should structure its user interface. Notice how it calls YOUR SetOperationsLab methods!
    /// </summary>
    public static class LabSupport
    {
        // ============================================
        // 🎪 INTERACTIVE MENU SYSTEM
        // ============================================

        /// <summary>
        /// Main application loop that manages the interactive lab experience
        /// 
        /// INSTRUCTOR NOTE: This is the heart of the user interface - a standard pattern
        /// for console applications. Notice the structure:
        /// 1. Welcome message and context setting
        /// 2. Infinite loop with menu display and input handling
        /// 3. Comprehensive try-catch for error handling and learning feedback
        /// 4. Graceful exit with session statistics
        /// 
        /// This pattern allows students to experiment freely without crashes
        /// while providing immediate feedback on their TODO implementations.
        /// </summary>
        /// <param name="lab">The SetOperationsLab instance containing student implementations</param>
        public static void RunInteractiveMenu(SetOperationsLab lab)
        {
            // INSTRUCTOR NOTE: Always start with a clear welcome that sets expectations
            // and provides context for what students will be learning
            Console.WriteLine("🎯 Welcome to the Set Operations Lab!");
            Console.WriteLine("This system demonstrates how HashSet<T> powers modern applications.\n");

            // INSTRUCTOR NOTE: The main application loop - keep running until user exits
            // This allows students to test their implementations multiple times
            while (true)
            {
                DisplayMainMenu(lab);
                var choice = Console.ReadLine()?.Trim();
                Console.WriteLine();

                // INSTRUCTOR NOTE: Wrap all student code execution in try-catch
                // This prevents crashes from NotImplementedException and provides learning feedback
                try
                {
                    // INSTRUCTOR NOTE: Support both numbers and words for accessibility
                    // Students can type "1" or "email" - both work the same way
                    switch (choice)
                    {
                        case "1": case "email": HandleEmailDeduplication(lab); break;
                        case "2": case "permission": HandlePermissionCheck(lab); break;
                        case "3": case "grant": HandleGrantPermissions(lab); break;
                        case "4": case "validate": HandleValidateAccess(lab); break;
                        case "5": case "new": HandleFindNewStudents(lab); break;
                        case "6": case "dropped": HandleFindDroppedStudents(lab); break;
                        case "7": case "continuing": HandleFindContinuingStudents(lab); break;
                        case "8": case "retention": HandleCalculateRetention(lab); break;
                        case "9": case "stats": HandleShowStats(lab); break;
                        case "0":
                        case "exit":
                            // INSTRUCTOR NOTE: Provide meaningful session summary on exit
                            var stats = lab.GetSystemStats();
                            Console.WriteLine("👋 Thanks for exploring sets in C#!");
                            Console.WriteLine($"📊 You performed {stats.TotalOperations} set operations during this session!");
                            return;
                        default:
                            Console.WriteLine("❌ Invalid option. Try again!");
                            break;
                    }
                }
                catch (NotImplementedException ex)
                {
                    // INSTRUCTOR NOTE: Special handling for TODO methods - this is educational feedback!
                    // When students haven't implemented a method yet, guide them constructively
                    Console.WriteLine($"🚧 {ex.Message}");
                    Console.WriteLine("💡 This feature will work once you implement the TODO method!\n");
                }
                catch (Exception ex)
                {
                    // INSTRUCTOR NOTE: Handle any other errors gracefully for a smooth learning experience
                    Console.WriteLine($"❌ Error: {ex.Message}\n");
                }

                // INSTRUCTOR NOTE: Pause for user feedback and clear screen for clean experience
                Console.WriteLine("Press any key to continue...");
                Console.ReadKey();
                Console.Clear();
            }
        }

        /// <summary>
        /// Display the main menu with current system statistics
        /// 
        /// INSTRUCTOR NOTE: This method demonstrates good UI design principles:
        /// 1. Clear visual hierarchy with ASCII box drawing
        /// 2. Real-time status information (operation count, data status)
        /// 3. Multiple input methods for accessibility
        /// 4. Consistent formatting that's easy to scan
        /// 
        /// The menu updates dynamically to show student progress through the lab.
        /// </summary>
        /// <param name="lab">Lab instance to get current statistics from</param>
        private static void DisplayMainMenu(SetOperationsLab lab)
        {
            // INSTRUCTOR NOTE: Get live statistics from the student's lab implementation
            // This provides immediate feedback on their progress and system state
            var stats = lab.GetSystemStats();
            
            // INSTRUCTOR NOTE: ASCII box drawing creates a professional console interface
            // that's easy to read and navigate during live coding sessions
            Console.WriteLine("┌─ Set Operations Lab Menu ──────────────────────────┐");
            Console.WriteLine("│ 1. Email Deduplication │ 2. Check Permission      │");
            Console.WriteLine("│ 3. Grant Permissions   │ 4. Validate Access       │");
            Console.WriteLine("│ 5. Find New Students   │ 6. Find Dropped Students │");
            Console.WriteLine("│ 7. Continuing Students │ 8. Retention Rate        │");
            Console.WriteLine("│ 9. Show Stats          │ 0. Exit                  │");
            Console.WriteLine("└─────────────────────────────────────────────────────┘");
            
            // INSTRUCTOR NOTE: Show dynamic status to help students track their progress
            Console.WriteLine($"Operations: {stats.TotalOperations} | Demo Data Loaded ✅");
            Console.WriteLine();
            Console.Write("Choose operation (number or name): ");
        }

        // ============================================
        // 🎭 MENU HANDLERS
        // ============================================

        /// <summary>
        /// Demonstrate email deduplication using HashSet operations
        /// 
        /// INSTRUCTOR NOTE: This method showcases the most fundamental HashSet use case:
        /// automatic duplicate removal. It calls the student's DeduplicateEmails method
        /// and provides visual feedback showing before/after states.
        /// 
        /// Key learning concepts:
        /// - HashSet automatically handles duplicates
        /// - Case-insensitive comparison with StringComparer.OrdinalIgnoreCase
        /// - Practical real-world application (email list cleanup)
        /// </summary>
        /// <param name="lab">Lab instance containing student's DeduplicateEmails implementation</param>
        public static void HandleEmailDeduplication(SetOperationsLab lab)
        {
            Console.WriteLine("📧 Email Deduplication Demo");
            Console.WriteLine("===========================");

            // INSTRUCTOR NOTE: Create demo data that clearly shows the deduplication problem
            // Includes exact duplicates, case variations, and unique emails for testing
            var demoEmails = CreateDemoEmailList();
            Console.WriteLine($"📋 Original email list ({demoEmails.Count} emails):");
            foreach (var email in demoEmails)
            {
                Console.WriteLine($"  📧 {email}");
            }
            Console.WriteLine();

            // INSTRUCTOR NOTE: This calls the student's DeduplicateEmails method!
            // The method modifies the list in-place and returns the count of duplicates removed
            int duplicatesRemoved = lab.DeduplicateEmails(demoEmails);
            
            // INSTRUCTOR NOTE: Provide clear feedback showing the results of their implementation
            Console.WriteLine($"✅ Deduplication complete!");
            Console.WriteLine($"🗑️ Duplicates removed: {duplicatesRemoved}");
            Console.WriteLine($"📊 Unique emails: {demoEmails.Count - duplicatesRemoved}");
        }

        /// <summary>
        /// Demonstrate permission checking using HashSet membership testing
        /// 
        /// INSTRUCTOR NOTE: This method shows HashSet's O(1) lookup performance in action.
        /// It demonstrates a common security pattern - checking if a user has a specific
        /// permission before allowing an operation.
        /// 
        /// Key learning concepts:
        /// - Contains() method for O(1) membership testing
        /// - Real-world security applications
        /// - Boolean logic in permission systems
        /// </summary>
        /// <param name="lab">Lab instance containing student's HasPermission implementation</param>
        public static void HandlePermissionCheck(SetOperationsLab lab)
        {
            Console.WriteLine("🔍 Permission Check Demo");
            Console.WriteLine("========================");

            // INSTRUCTOR NOTE: Use realistic test data that students can relate to
            string userId = "alice@company.com";
            string permission = "admin";

            Console.WriteLine($"👤 Checking if {userId} has '{permission}' permission...");
            
            // INSTRUCTOR NOTE: This calls the student's HasPermission method!
            // The method should use HashSet.Contains() for O(1) lookup performance
            bool hasAccess = lab.HasPermission(userId, permission);
            var icon = hasAccess ? "✅" : "❌";
            Console.WriteLine($"{icon} Result: {(hasAccess ? "GRANTED" : "DENIED")}");

            // INSTRUCTOR NOTE: Show context by displaying the user's current permissions
            // This helps students understand the data structure and verify their implementation
            var userPermissions = lab.GetUserPermissions();
            if (userPermissions.ContainsKey(userId))
            {
                Console.WriteLine($"\n🔑 {userId} current permissions:");
                foreach (var perm in userPermissions[userId])
                {
                    Console.WriteLine($"  ✅ {perm}");
                }
            }
        }

        /// <summary>
        /// Demonstrate adding permissions using HashSet union operations
        /// 
        /// INSTRUCTOR NOTE: This method showcases HashSet's UnionWith() method or
        /// the Add() method for building permission sets. It demonstrates how
        /// HashSet automatically prevents duplicate permissions.
        /// 
        /// Key learning concepts:
        /// - Set union operations (combining sets)
        /// - Automatic duplicate prevention
        /// - Counting actual additions vs. attempted additions
        /// - Permission accumulation patterns
        /// </summary>
        /// <param name="lab">Lab instance containing student's AddPermissions implementation</param>
        public static void HandleGrantPermissions(SetOperationsLab lab)
        {
            Console.WriteLine("➕ Grant Permissions Demo");
            Console.WriteLine("=========================");

            // INSTRUCTOR NOTE: Test adding both new and existing permissions
            // This demonstrates HashSet's duplicate handling behavior
            string userId = "bob@company.com";
            var newPermissions = new HashSet<string> { "export", "audit", "admin" };

            Console.WriteLine($"👤 User: {userId}");
            Console.WriteLine($"🎁 Granting permissions: [{string.Join(", ", newPermissions)}]");

            // INSTRUCTOR NOTE: This calls the student's AddPermissions method!
            // Should return count of actually added permissions (excluding duplicates)
            int permissionsAdded = lab.AddPermissions(userId, newPermissions);
            Console.WriteLine($"✅ Successfully added {permissionsAdded} new permissions!");

            // INSTRUCTOR NOTE: Show the updated state to verify the implementation worked
            var userPermissions = lab.GetUserPermissions();
            if (userPermissions.ContainsKey(userId))
            {
                Console.WriteLine($"\n🔑 {userId} updated permissions:");
                foreach (var perm in userPermissions[userId])
                {
                    Console.WriteLine($"  ✅ {perm}");
                }
            }
        }

        /// <summary>
        /// Demonstrate access validation using set difference operations
        /// 
        /// INSTRUCTOR NOTE: This method teaches the ExceptWith() operation or manual
        /// iteration to find missing elements. It's a common security pattern:
        /// "What permissions does this user still need?"
        /// 
        /// Key learning concepts:
        /// - Set difference operations (what's in A but not in B)
        /// - Security validation patterns
        /// - Access control decision making
        /// - Empty set handling for successful validation
        /// </summary>
        /// <param name="lab">Lab instance containing student's GetMissingPermissions implementation</param>
        public static void HandleValidateAccess(SetOperationsLab lab)
        {
            Console.WriteLine("🎯 Access Validation Demo");
            Console.WriteLine("=========================");

            // INSTRUCTOR NOTE: Create a realistic scenario with some missing permissions
            string userId = "charlie@company.com";
            var requiredPermissions = new HashSet<string> { "read", "write", "export" };

            Console.WriteLine($"👤 User: {userId}");
            Console.WriteLine($"🔐 Required permissions: [{string.Join(", ", requiredPermissions)}]");

            // INSTRUCTOR NOTE: This calls the student's GetMissingPermissions method!
            // Should return a HashSet of permissions the user doesn't have
            var missingPermissions = lab.GetMissingPermissions(userId, requiredPermissions);

            // INSTRUCTOR NOTE: Demonstrate conditional logic based on set operations
            if (missingPermissions.Count == 0)
            {
                Console.WriteLine("✅ ACCESS GRANTED - User has all required permissions!");
            }
            else
            {
                Console.WriteLine("❌ ACCESS DENIED - Missing permissions:");
                foreach (var perm in missingPermissions)
                {
                    Console.WriteLine($"  ❌ {perm}");
                }
            }

            // INSTRUCTOR NOTE: Show current permissions for context and debugging
            var userPermissions = lab.GetUserPermissions();
            if (userPermissions.ContainsKey(userId))
            {
                Console.WriteLine($"\n🔑 {userId} current permissions:");
                foreach (var perm in userPermissions[userId])
                {
                    Console.WriteLine($"  ✅ {perm}");
                }
            }
        }

        /// <summary>
        /// Demonstrate finding new students using set difference operations
        /// 
        /// INSTRUCTOR NOTE: This method teaches set difference in a practical context:
        /// "Who is in this quarter but not last quarter?" It uses the ExceptWith()
        /// operation or manual comparison to find new enrollments.
        /// 
        /// Key learning concepts:
        /// - Set difference (A - B = elements in A but not in B)
        /// - Real-world data analysis applications
        /// - Enrollment change tracking
        /// - Business intelligence with sets
        /// </summary>
        /// <param name="lab">Lab instance containing student's FindNewStudents implementation</param>
        public static void HandleFindNewStudents(SetOperationsLab lab)
        {
            Console.WriteLine("🆕 New Students Analysis");
            Console.WriteLine("========================");

            // INSTRUCTOR NOTE: This calls the student's FindNewStudents method!
            // Should return students in current quarter but not in previous quarter
            var newStudents = lab.FindNewStudents();
            var (enrolledNow, enrolledLastQuarter) = lab.GetEnrollmentData();

            // INSTRUCTOR NOTE: Show the data context first, then the analysis results
            Console.WriteLine($"📊 Analysis Results:");
            Console.WriteLine($"📅 Last Quarter: {enrolledLastQuarter.Count} students");
            Console.WriteLine($"📅 This Quarter: {enrolledNow.Count} students");
            Console.WriteLine($"🆕 New Students: {newStudents.Count}");

            // INSTRUCTOR NOTE: Handle both cases - with and without new students
            if (newStudents.Count > 0)
            {
                Console.WriteLine("\n🎉 Welcome new students:");
                foreach (var student in newStudents)
                {
                    Console.WriteLine($"  📚 {student}");
                }
            }
            else
            {
                Console.WriteLine("\n📋 No new students this quarter.");
            }
        }

        /// <summary>
        /// Demonstrate finding dropped students using set difference operations
        /// 
        /// INSTRUCTOR NOTE: This method teaches the inverse of finding new students:
        /// "Who was in last quarter but not this quarter?" It's the same set difference
        /// concept but applied in the opposite direction.
        /// 
        /// Key learning concepts:
        /// - Inverse set difference (B - A instead of A - B)
        /// - Student retention analysis
        /// - Understanding set operations are not commutative
        /// - Practical applications of set theory
        /// </summary>
        /// <param name="lab">Lab instance containing student's FindDroppedStudents implementation</param>
        public static void HandleFindDroppedStudents(SetOperationsLab lab)
        {
            Console.WriteLine("🚪 Dropped Students Analysis");
            Console.WriteLine("============================");

            // INSTRUCTOR NOTE: This calls the student's FindDroppedStudents method!
            // Should return students in last quarter but not in current quarter
            var droppedStudents = lab.FindDroppedStudents();
            var (enrolledNow, enrolledLastQuarter) = lab.GetEnrollmentData();

            // INSTRUCTOR NOTE: Provide context for understanding the analysis
            Console.WriteLine($"📊 Analysis Results:");
            Console.WriteLine($"📅 Last Quarter: {enrolledLastQuarter.Count} students");
            Console.WriteLine($"📅 This Quarter: {enrolledNow.Count} students");
            Console.WriteLine($"🚪 Dropped Students: {droppedStudents.Count}");

            // INSTRUCTOR NOTE: Show results with appropriate messaging for both outcomes
            if (droppedStudents.Count > 0)
            {
                Console.WriteLine("\n👋 Students who left:");
                foreach (var student in droppedStudents)
                {
                    Console.WriteLine($"  📤 {student}");
                }
            }
            else
            {
                Console.WriteLine("\n📋 No students dropped this quarter.");
            }
        }

        /// <summary>
        /// Demonstrate finding continuing students using set intersection operations
        /// 
        /// INSTRUCTOR NOTE: This method teaches set intersection - one of the most
        /// important set operations. It finds elements that exist in BOTH sets,
        /// representing student loyalty and retention.
        /// 
        /// Key learning concepts:
        /// - Set intersection (A ∩ B = elements in both A and B)
        /// - IntersectWith() method usage
        /// - Student retention metrics
        /// - Finding commonalities between datasets
        /// </summary>
        /// <param name="lab">Lab instance containing student's FindContinuingStudents implementation</param>
        public static void HandleFindContinuingStudents(SetOperationsLab lab)
        {
            Console.WriteLine("🔄 Continuing Students Analysis");
            Console.WriteLine("===============================");

            // INSTRUCTOR NOTE: This calls the student's FindContinuingStudents method!
            // Should return students who appear in BOTH quarters (intersection)
            var continuingStudents = lab.FindContinuingStudents();
            var (enrolledNow, enrolledLastQuarter) = lab.GetEnrollmentData();

            // INSTRUCTOR NOTE: Show comprehensive statistics for enrollment analysis
            Console.WriteLine($"📊 Analysis Results:");
            Console.WriteLine($"📅 Last Quarter: {enrolledLastQuarter.Count} students");
            Console.WriteLine($"📅 This Quarter: {enrolledNow.Count} students");
            Console.WriteLine($"🔄 Continuing Students: {continuingStudents.Count}");

            // INSTRUCTOR NOTE: Celebrate continuing students - they represent successful retention
            if (continuingStudents.Count > 0)
            {
                Console.WriteLine("\n📚 Loyal students continuing:");
                foreach (var student in continuingStudents)
                {
                    Console.WriteLine($"  ✅ {student}");
                }
            }
            else
            {
                Console.WriteLine("\n📋 No continuing students found.");
            }
        }

        /// <summary>
        /// Demonstrate calculating retention rate using set operations and mathematics
        /// 
        /// INSTRUCTOR NOTE: This method combines set operations with business metrics.
        /// It shows how HashSet operations support data analysis and KPI calculations
        /// in real-world applications.
        /// 
        /// Key learning concepts:
        /// - Combining set operations with mathematical calculations
        /// - Business metrics derived from set operations
        /// - Percentage calculations and formatting
        /// - Conditional feedback based on calculated results
        /// </summary>
        /// <param name="lab">Lab instance containing student's CalculateRetentionRate implementation</param>
        public static void HandleCalculateRetention(SetOperationsLab lab)
        {
            Console.WriteLine("📈 Retention Rate Analysis");
            Console.WriteLine("==========================");

            // INSTRUCTOR NOTE: This calls the student's CalculateRetentionRate method!
            // Should use intersection count divided by last quarter count * 100
            double retentionRate = lab.CalculateRetentionRate();
            var (enrolledNow, enrolledLastQuarter) = lab.GetEnrollmentData();

            // INSTRUCTOR NOTE: Show all the underlying data for transparency and learning
            Console.WriteLine($"📊 Enrollment Statistics:");
            Console.WriteLine($"📅 Last Quarter: {enrolledLastQuarter.Count} students");
            Console.WriteLine($"📅 This Quarter: {enrolledNow.Count} students");
            Console.WriteLine($"📈 Retention Rate: {retentionRate:F1}%");

            // INSTRUCTOR NOTE: Provide contextual feedback based on the calculated rate
            // This teaches students about business interpretation of technical results
            if (retentionRate >= 80)
            {
                Console.WriteLine("🌟 Excellent retention rate!");
            }
            else if (retentionRate >= 60)
            {
                Console.WriteLine("✅ Good retention rate.");
            }
            else
            {
                Console.WriteLine("⚠️ Low retention - may need intervention.");
            }
        }

        /// <summary>
        /// Display comprehensive system statistics and HashSet operation reference
        /// 
        /// INSTRUCTOR NOTE: This method serves multiple educational purposes:
        /// 1. Shows students their progress through the lab
        /// 2. Provides a quick reference for HashSet operations
        /// 3. Demonstrates system state tracking
        /// 4. Reinforces Big O notation concepts
        /// 
        /// It's both a progress indicator and a learning reference tool.
        /// </summary>
        /// <param name="lab">Lab instance containing system statistics</param>
        public static void HandleShowStats(SetOperationsLab lab)
        {
            Console.WriteLine("📊 System Statistics");
            Console.WriteLine("====================");

            // INSTRUCTOR NOTE: Get comprehensive statistics from the student's implementation
            var stats = lab.GetSystemStats();

            // INSTRUCTOR NOTE: Display session progress and system state information
            Console.WriteLine($"🎯 Total Set Operations: {stats.TotalOperations}");
            Console.WriteLine($"⏱️ Lab Session Time: {stats.Uptime.ToString("hh\\:mm\\:ss")}");
            Console.WriteLine($"📧 Unique Emails Tracked: {stats.UniqueEmailsCount}");
            Console.WriteLine($"👥 Users with Permissions: {stats.UsersWithPermissionsCount}");
            Console.WriteLine($"📅 This Quarter Enrollment: {stats.ThisQuarterEnrollment}");
            Console.WriteLine($"📅 Last Quarter Enrollment: {stats.LastQuarterEnrollment}");

            // INSTRUCTOR NOTE: Provide a quick reference guide for HashSet operations
            // This reinforces the key concepts students are learning
            Console.WriteLine("\n🎯 Set Operations Available:");
            Console.WriteLine("  ✅ Add() - O(1) insertion");
            Console.WriteLine("  ✅ Contains() - O(1) membership testing");
            Console.WriteLine("  ✅ Remove() - O(1) deletion");
            Console.WriteLine("  ✅ UnionWith() - Merge sets");
            Console.WriteLine("  ✅ IntersectWith() - Find common elements");
            Console.WriteLine("  ✅ ExceptWith() - Find differences");
            Console.WriteLine("  ✅ IsSubsetOf() - Check containment");
        }

        // ============================================
        // 🎭 DEMO DATA SETUP
        // ============================================

        /// <summary>
        /// Initialize all demo data for the lab session
        /// 
        /// INSTRUCTOR NOTE: Demo data is crucial for effective lab sessions. This method
        /// sets up realistic test scenarios that allow students to immediately see their
        /// implementations in action without having to manually create test data.
        /// 
        /// The data is designed to showcase specific HashSet behaviors:
        /// - Email duplicates (exact and case-insensitive)
        /// - Permission hierarchies and overlaps
        /// - Student enrollment changes between quarters
        /// </summary>
        /// <param name="lab">Lab instance to populate with demo data</param>
        public static void LoadDemoData(SetOperationsLab lab)
        {
            // INSTRUCTOR NOTE: Load demo email data with intentional duplicates
            // This tests case-insensitive deduplication and exact duplicate removal
            var demoEmails = CreateDemoEmailList();
            lab.InitializeUniqueEmails(demoEmails);

            // INSTRUCTOR NOTE: Load demo permission data with varied access levels
            // This creates realistic scenarios for permission testing and validation
            var permissionData = CreateDemoPermissionData();
            lab.InitializeUserPermissions(permissionData);

            // INSTRUCTOR NOTE: Load enrollment data showing student movement between quarters
            // This enables meaningful analysis of retention, new students, and dropouts
            var (thisQuarter, lastQuarter) = CreateDemoEnrollmentData();
            lab.InitializeEnrollmentData(thisQuarter, lastQuarter);
        }

        /// <summary>
        /// Create demo email list with intentional duplicates for testing deduplication
        /// 
        /// INSTRUCTOR NOTE: This data is carefully designed to test different aspects
        /// of HashSet behavior with StringComparer.OrdinalIgnoreCase:
        /// 
        /// 1. Exact duplicates: "alice@email.com" appears multiple times
        /// 2. Case variations: "ALICE@EMAIL.COM", "Bob@Company.Co" 
        /// 3. Unique emails: Some emails appear only once
        /// 
        /// This allows students to see both duplicate removal and case-insensitive
        /// comparison in action during the deduplication demo.
        /// </summary>
        /// <returns>List containing emails with intentional duplicates</returns>
        public static List<string> CreateDemoEmailList()
        {
            return new List<string>
            {
                // INSTRUCTOR NOTE: Original unique emails - these should remain after deduplication
                "alice@email.com",
                "bob@company.co",
                "charlie@university.edu",
                "diana@music.com",
                "edward@theater.org",
                
                // INSTRUCTOR NOTE: Case variations (duplicates) - should be removed by case-insensitive HashSet
                "ALICE@EMAIL.COM",        // Duplicate of alice@email.com
                "Bob@Company.Co",         // Duplicate of bob@company.co
                "CHARLIE@UNIVERSITY.EDU", // Duplicate of charlie@university.edu
                
                // INSTRUCTOR NOTE: Exact duplicates - should be removed by HashSet
                "alice@email.com",        // Exact duplicate
                "bob@company.co",         // Exact duplicate
                "diana@music.com"         // Exact duplicate
            };
        }

        /// <summary>
        /// Create demo permission data with varied access levels for testing
        /// 
        /// INSTRUCTOR NOTE: This data creates realistic permission scenarios:
        /// 
        /// - alice: Full admin access (read, write, admin)
        /// - bob: Standard user (read, write) - missing admin
        /// - charlie: Restricted user (read only) - missing write and admin
        /// - diana: Super user (all permissions including delete)
        /// 
        /// This variety allows testing all permission-related methods with
        /// meaningful and predictable results.
        /// </summary>
        /// <returns>Dictionary mapping usernames to their permission sets</returns>
        private static Dictionary<string, HashSet<string>> CreateDemoPermissionData()
        {
            return new Dictionary<string, HashSet<string>>
            {
                // INSTRUCTOR NOTE: Alice has admin access - good for permission testing
                ["alice@company.com"] = new HashSet<string> { "read", "write", "admin" },
                
                // INSTRUCTOR NOTE: Bob has standard access - will be used for permission granting demo
                ["bob@company.com"] = new HashSet<string> { "read", "write" },
                
                // INSTRUCTOR NOTE: Charlie has minimal access - good for missing permission testing
                ["charlie@company.com"] = new HashSet<string> { "read" },
                
                // INSTRUCTOR NOTE: Diana has maximum access including rare "delete" permission
                ["diana@company.com"] = new HashSet<string> { "read", "write", "admin", "delete" }
            };
        }

        /// <summary>
        /// Create demo enrollment data showing student movement between quarters
        /// 
        /// INSTRUCTOR NOTE: This data is designed to demonstrate all types of enrollment changes:
        /// 
        /// Student Categories:
        /// - Continuing: alice, bob, diana, frank (in both quarters)
        /// - Dropped: charlie, edward (in last quarter only)  
        /// - New: grace, henry, iris (in this quarter only)
        /// 
        /// This creates meaningful results for all student analysis methods:
        /// - 4 continuing students (retention rate calculation)
        /// - 2 dropped students (attrition analysis)
        /// - 3 new students (growth analysis)
        /// - Different total enrollments (6 → 7 shows net growth)
        /// </summary>
        /// <returns>Tuple of (current quarter, previous quarter) enrollment sets</returns>
        private static (HashSet<string> thisQuarter, HashSet<string> lastQuarter) CreateDemoEnrollmentData()
        {
            // INSTRUCTOR NOTE: Last quarter enrollment - the baseline for comparison
            var lastQuarter = new HashSet<string>
            {
                "alice@student.edu",   // Continuing student
                "bob@student.edu",     // Continuing student  
                "charlie@student.edu", // Dropped student (not in current)
                "diana@student.edu",   // Continuing student
                "edward@student.edu",  // Dropped student (not in current)
                "frank@student.edu"    // Continuing student
            };

            // INSTRUCTOR NOTE: This quarter enrollment - shows changes from last quarter
            var thisQuarter = new HashSet<string>
            {
                "alice@student.edu",  // Continuing (was in last quarter)
                "bob@student.edu",    // Continuing (was in last quarter)
                "diana@student.edu",  // Continuing (was in last quarter)
                "frank@student.edu",  // Continuing (was in last quarter)
                "grace@student.edu",  // New student (not in last quarter)
                "henry@student.edu",  // New student (not in last quarter)
                "iris@student.edu"    // New student (not in last quarter)
            };

            return (thisQuarter, lastQuarter);
        }
    }

    // ============================================
    // 📊 HELPER CLASSES
    // ============================================

    /// <summary>
    /// Data transfer object for system statistics and session tracking
    /// 
    /// INSTRUCTOR NOTE: This class demonstrates the Data Transfer Object (DTO) pattern.
    /// It provides a clean interface for passing statistical information between
    /// the core lab logic and the UI layer.
    /// 
    /// Key concepts:
    /// - Separation of data structure from business logic
    /// - Clean API design for statistical reporting
    /// - Property-based data access pattern
    /// - Session tracking for educational progress monitoring
    /// </summary>
    public class SystemStats
    {
        /// <summary>Total number of set operations performed during this session</summary>
        public int TotalOperations { get; set; }
        
        /// <summary>Time elapsed since the lab session started</summary>
        public TimeSpan Uptime { get; set; }
        
        /// <summary>Number of unique email addresses currently tracked</summary>
        public int UniqueEmailsCount { get; set; }
        
        /// <summary>Number of users who have at least one permission</summary>
        public int UsersWithPermissionsCount { get; set; }
        
        /// <summary>Total enrollment count for the current quarter</summary>
        public int ThisQuarterEnrollment { get; set; }
        
        /// <summary>Total enrollment count for the previous quarter</summary>
        public int LastQuarterEnrollment { get; set; }
    }
}