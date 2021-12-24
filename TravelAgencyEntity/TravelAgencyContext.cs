using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

#nullable disable

namespace TravelAgencyEntity
{
    public partial class TravelAgencyContext : DbContext
    {
        public TravelAgencyContext()
        {
        }

        public TravelAgencyContext(DbContextOptions<TravelAgencyContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Activity> Activities { get; set; }
        public virtual DbSet<Agent> Agents { get; set; }
        public virtual DbSet<AssignGuide> AssignGuides { get; set; }
        public virtual DbSet<Concert> Concerts { get; set; }
        public virtual DbSet<Customer> Customers { get; set; }
        public virtual DbSet<Discount> Discounts { get; set; }
        public virtual DbSet<Employee> Employees { get; set; }
        public virtual DbSet<Feedback> Feedbacks { get; set; }
        public virtual DbSet<Festival> Festivals { get; set; }
        public virtual DbSet<Guide> Guides { get; set; }
        public virtual DbSet<GuideReview> GuideReviews { get; set; }
        public virtual DbSet<GuidesFeedback> GuidesFeedbacks { get; set; }
        public virtual DbSet<Hotel> Hotels { get; set; }
        public virtual DbSet<HotelReservation> HotelReservations { get; set; }
        public virtual DbSet<Language> Languages { get; set; }
        public virtual DbSet<MarkedActivity> MarkedActivities { get; set; }
        public virtual DbSet<Reservation> Reservations { get; set; }
        public virtual DbSet<Review> Reviews { get; set; }
        public virtual DbSet<Room> Rooms { get; set; }
        public virtual DbSet<SightseeingPlace> SightseeingPlaces { get; set; }
        public virtual DbSet<Speak> Speaks { get; set; }
        public virtual DbSet<Tour> Tours { get; set; }
        public virtual DbSet<TourReservation> TourReservations { get; set; }
        public virtual DbSet<TourReview> TourReviews { get; set; }
        public virtual DbSet<User> Users { get; set; }
        public virtual DbSet<Visit> Visits { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer("Data Source=(localdb)\\MSSQLLocalDB;Initial Catalog=TravelAgency;Integrated Security=True;MultipleActiveResultSets=True;");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasAnnotation("Relational:Collation", "SQL_Latin1_General_CP1_CI_AS");

            modelBuilder.Entity<Activity>(entity =>
            {
                entity.ToTable("Activity");

                entity.Property(e => e.ActivityId)
                    .ValueGeneratedNever()
                    .HasColumnName("activity_id");

                entity.Property(e => e.ADescription)
                    .IsUnicode(false)
                    .HasColumnName("a_description");

                entity.Property(e => e.ActivityEndTime)
                    .HasColumnType("datetime")
                    .HasColumnName("activity_end_time");

                entity.Property(e => e.ActivityName)
                    .HasMaxLength(64)
                    .IsUnicode(false)
                    .HasColumnName("activity_name");

                entity.Property(e => e.ActivityStartTime)
                    .HasColumnType("datetime")
                    .HasColumnName("activity_start_time");

                entity.Property(e => e.DiscountId).HasColumnName("discount_id");

                entity.Property(e => e.TicketPrice)
                    .HasColumnType("decimal(8, 2)")
                    .HasColumnName("ticket_price");

                entity.Property(e => e.TourId).HasColumnName("tour_id");

                entity.HasOne(d => d.Discount)
                    .WithMany(p => p.Activities)
                    .HasForeignKey(d => d.DiscountId)
                    .HasConstraintName("FK__Activity__discou__412EB0B6");

                entity.HasOne(d => d.Tour)
                    .WithMany(p => p.Activities)
                    .HasForeignKey(d => d.TourId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__Activity__tour_i__403A8C7D");
            });

            modelBuilder.Entity<Agent>(entity =>
            {
                entity.HasKey(e => e.UId)
                    .HasName("PK__Agent__B51D3DEAB9EC4CC5");

                entity.ToTable("Agent");

                entity.Property(e => e.UId)
                    .ValueGeneratedNever()
                    .HasColumnName("u_id");

                entity.HasOne(d => d.UIdNavigation)
                    .WithOne(p => p.Agent)
                    .HasForeignKey<Agent>(d => d.UId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__Agent__u_id__32E0915F");
            });

            modelBuilder.Entity<AssignGuide>(entity =>
            {
                entity.HasKey(e => e.TourId)
                    .HasName("PK__assign_g__4B16B9E6BB2E721A");

                entity.ToTable("assign_guide");

                entity.Property(e => e.TourId)
                    .ValueGeneratedNever()
                    .HasColumnName("tour_id");

                entity.Property(e => e.AgentUId).HasColumnName("agent_u_id");

                entity.Property(e => e.AssignStatus)
                    .HasMaxLength(10)
                    .IsUnicode(false)
                    .HasColumnName("assign_status");

                entity.Property(e => e.GuideUId).HasColumnName("guide_u_id");

                entity.HasOne(d => d.AgentU)
                    .WithMany(p => p.AssignGuides)
                    .HasForeignKey(d => d.AgentUId)
                    .HasConstraintName("FK__assign_gu__agent__6C190EBB");

                entity.HasOne(d => d.GuideU)
                    .WithMany(p => p.AssignGuides)
                    .HasForeignKey(d => d.GuideUId)
                    .HasConstraintName("FK__assign_gu__guide__6D0D32F4");

                entity.HasOne(d => d.Tour)
                    .WithOne(p => p.AssignGuide)
                    .HasForeignKey<AssignGuide>(d => d.TourId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__assign_gu__tour___6E01572D");
            });

            modelBuilder.Entity<Concert>(entity =>
            {
                entity.HasKey(e => e.ActivityId)
                    .HasName("PK__Concert__482FBD63D55BD28D");

                entity.ToTable("Concert");

                entity.Property(e => e.ActivityId)
                    .ValueGeneratedNever()
                    .HasColumnName("activity_id");

                entity.Property(e => e.ArtistName)
                    .IsUnicode(false)
                    .HasColumnName("artist_name");

                entity.Property(e => e.Genre)
                    .HasMaxLength(64)
                    .IsUnicode(false)
                    .HasColumnName("genre");

                entity.HasOne(d => d.Activity)
                    .WithOne(p => p.Concert)
                    .HasForeignKey<Concert>(d => d.ActivityId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__Concert__activit__46E78A0C");
            });

            modelBuilder.Entity<Customer>(entity =>
            {
                entity.HasKey(e => e.UId)
                    .HasName("PK__Customer__B51D3DEAC85A6583");

                entity.ToTable("Customer");

                entity.Property(e => e.UId)
                    .ValueGeneratedNever()
                    .HasColumnName("u_id");

                entity.Property(e => e.CAddress)
                    .IsUnicode(false)
                    .HasColumnName("c_address");

                entity.Property(e => e.Wallet)
                    .HasColumnType("decimal(8, 2)")
                    .HasColumnName("wallet");

                entity.HasOne(d => d.UIdNavigation)
                    .WithOne(p => p.Customer)
                    .HasForeignKey<Customer>(d => d.UId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__Customer__u_id__2A4B4B5E");
            });

            modelBuilder.Entity<Discount>(entity =>
            {
                entity.ToTable("Discount");

                entity.Property(e => e.DiscountId)
                    .ValueGeneratedNever()
                    .HasColumnName("discount_id");

                entity.Property(e => e.DiscountStartTime)
                    .HasColumnType("datetime")
                    .HasColumnName("discount_start_time");

                entity.Property(e => e.DiscountTimeInterval).HasColumnName("discount_time_interval");

                entity.Property(e => e.DiscountType)
                    .HasMaxLength(16)
                    .IsUnicode(false)
                    .HasColumnName("discount_type");

                entity.Property(e => e.Percents).HasColumnName("percents");
            });

            modelBuilder.Entity<Employee>(entity =>
            {
                entity.HasKey(e => e.UId)
                    .HasName("PK__Employee__B51D3DEACF4DADBC");

                entity.ToTable("Employee");

                entity.Property(e => e.UId)
                    .ValueGeneratedNever()
                    .HasColumnName("u_id");

                entity.Property(e => e.Salary)
                    .HasColumnType("decimal(8, 2)")
                    .HasColumnName("salary");

                entity.HasOne(d => d.UIdNavigation)
                    .WithOne(p => p.Employee)
                    .HasForeignKey<Employee>(d => d.UId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__Employee__u_id__2D27B809");
            });

            modelBuilder.Entity<Feedback>(entity =>
            {
                entity.ToTable("Feedback");

                entity.Property(e => e.FeedbackId)
                    .ValueGeneratedNever()
                    .HasColumnName("feedback_id");

                entity.Property(e => e.Content)
                    .IsUnicode(false)
                    .HasColumnName("content");

                entity.Property(e => e.FeedbackTime)
                    .HasColumnType("datetime")
                    .HasColumnName("feedback_time");
            });

            modelBuilder.Entity<Festival>(entity =>
            {
                entity.HasKey(e => e.ActivityId)
                    .HasName("PK__Festival__482FBD63C98FDD05");

                entity.ToTable("Festival");

                entity.Property(e => e.ActivityId)
                    .ValueGeneratedNever()
                    .HasColumnName("activity_id");

                entity.Property(e => e.AgeLimit).HasColumnName("age_limit");

                entity.Property(e => e.FoodCatalog)
                    .IsUnicode(false)
                    .HasColumnName("food_catalog");

                entity.HasOne(d => d.Activity)
                    .WithOne(p => p.Festival)
                    .HasForeignKey<Festival>(d => d.ActivityId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__Festival__activi__440B1D61");
            });

            modelBuilder.Entity<Guide>(entity =>
            {
                entity.HasKey(e => e.UId)
                    .HasName("PK__Guide__B51D3DEADF42419B");

                entity.ToTable("Guide");

                entity.Property(e => e.UId)
                    .ValueGeneratedNever()
                    .HasColumnName("u_id");

                entity.HasOne(d => d.UIdNavigation)
                    .WithOne(p => p.Guide)
                    .HasForeignKey<Guide>(d => d.UId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__Guide__u_id__300424B4");
            });

            modelBuilder.Entity<GuideReview>(entity =>
            {
                entity.HasKey(e => e.ReviewId)
                    .HasName("PK__GuideRev__60883D90A47A9A42");

                entity.ToTable("GuideReview");

                entity.Property(e => e.ReviewId)
                    .ValueGeneratedNever()
                    .HasColumnName("review_id");

                entity.Property(e => e.UId).HasColumnName("u_id");

                entity.HasOne(d => d.Review)
                    .WithOne(p => p.GuideReview)
                    .HasForeignKey<GuideReview>(d => d.ReviewId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__GuideRevi__revie__398D8EEE");

                entity.HasOne(d => d.UIdNavigation)
                    .WithMany(p => p.GuideReviews)
                    .HasForeignKey(d => d.UId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__GuideRevie__u_id__38996AB5");
            });

            modelBuilder.Entity<GuidesFeedback>(entity =>
            {
                entity.HasKey(e => e.FeedbackId)
                    .HasName("PK__guides_f__7A6B2B8C01ADA058");

                entity.ToTable("guides_feedback");

                entity.Property(e => e.FeedbackId)
                    .ValueGeneratedNever()
                    .HasColumnName("feedback_id");

                entity.Property(e => e.TourId).HasColumnName("tour_id");

                entity.Property(e => e.UId).HasColumnName("u_id");

                entity.HasOne(d => d.Feedback)
                    .WithOne(p => p.GuidesFeedback)
                    .HasForeignKey<GuidesFeedback>(d => d.FeedbackId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__guides_fe__feedb__6754599E");

                entity.HasOne(d => d.Tour)
                    .WithMany(p => p.GuidesFeedbacks)
                    .HasForeignKey(d => d.TourId)
                    .HasConstraintName("FK__guides_fe__tour___693CA210");

                entity.HasOne(d => d.UIdNavigation)
                    .WithMany(p => p.GuidesFeedbacks)
                    .HasForeignKey(d => d.UId)
                    .HasConstraintName("FK__guides_fee__u_id__68487DD7");
            });

            modelBuilder.Entity<Hotel>(entity =>
            {
                entity.ToTable("Hotel");

                entity.Property(e => e.HotelId)
                    .ValueGeneratedNever()
                    .HasColumnName("hotel_id");

                entity.Property(e => e.City)
                    .HasMaxLength(32)
                    .IsUnicode(false)
                    .HasColumnName("city");

                entity.Property(e => e.DiscountId).HasColumnName("discount_id");

                entity.Property(e => e.HotelName)
                    .HasMaxLength(64)
                    .IsUnicode(false)
                    .HasColumnName("hotel_name");

                entity.Property(e => e.NumOfStars).HasColumnName("num_of_stars");

                entity.HasOne(d => d.Discount)
                    .WithMany(p => p.Hotels)
                    .HasForeignKey(d => d.DiscountId)
                    .HasConstraintName("FK__Hotel__discount___4BAC3F29");
            });

            modelBuilder.Entity<HotelReservation>(entity =>
            {
                entity.HasKey(e => e.ReserveId)
                    .HasName("PK__HotelRes__BFAD1238893E9C3C");

                entity.ToTable("HotelReservation");

                entity.Property(e => e.ReserveId)
                    .ValueGeneratedNever()
                    .HasColumnName("reserve_id");

                entity.Property(e => e.HotelId).HasColumnName("hotel_id");

                entity.Property(e => e.ReserveEndDate)
                    .HasColumnType("date")
                    .HasColumnName("reserve_end_date");

                entity.Property(e => e.RoomId).HasColumnName("room_id");

                entity.HasOne(d => d.Reserve)
                    .WithOne(p => p.HotelReservation)
                    .HasForeignKey<HotelReservation>(d => d.ReserveId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__HotelRese__reser__59063A47");

                entity.HasOne(d => d.Room)
                    .WithMany(p => p.HotelReservations)
                    .HasForeignKey(d => new { d.RoomId, d.HotelId })
                    .HasConstraintName("FK__HotelReservation__5812160E");
            });

            modelBuilder.Entity<Language>(entity =>
            {
                entity.HasKey(e => e.LanguageName)
                    .HasName("PK__Language__9CE8238201D46FC2");

                entity.Property(e => e.LanguageName)
                    .HasMaxLength(16)
                    .IsUnicode(false)
                    .HasColumnName("language_name");
            });

            modelBuilder.Entity<MarkedActivity>(entity =>
            {
                entity.HasKey(e => new { e.ReserveId, e.ActivityId })
                    .HasName("PK__marked_a__9B2FE9EE0F25E44B");

                entity.ToTable("marked_activity");

                entity.Property(e => e.ReserveId).HasColumnName("reserve_id");

                entity.Property(e => e.ActivityId).HasColumnName("activity_id");

                entity.HasOne(d => d.Activity)
                    .WithMany(p => p.MarkedActivities)
                    .HasForeignKey(d => d.ActivityId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__marked_ac__activ__71D1E811");

                entity.HasOne(d => d.Reserve)
                    .WithMany(p => p.MarkedActivities)
                    .HasForeignKey(d => d.ReserveId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__marked_ac__reser__70DDC3D8");
            });

            modelBuilder.Entity<Reservation>(entity =>
            {
                entity.HasKey(e => e.ReserveId)
                    .HasName("PK__Reservat__BFAD1238E25227DF");

                entity.ToTable("Reservation");

                entity.Property(e => e.ReserveId)
                    .ValueGeneratedNever()
                    .HasColumnName("reserve_id");

                entity.Property(e => e.IsBooked).HasColumnName("is_booked");

                entity.Property(e => e.NumReserving).HasColumnName("num_reserving");

                entity.Property(e => e.ReserveStartDate)
                    .HasColumnType("date")
                    .HasColumnName("reserve_start_date");

                entity.Property(e => e.UId).HasColumnName("u_id");

                entity.HasOne(d => d.UIdNavigation)
                    .WithMany(p => p.Reservations)
                    .HasForeignKey(d => d.UId)
                    .HasConstraintName("FK__Reservatio__u_id__5165187F");
            });

            modelBuilder.Entity<Review>(entity =>
            {
                entity.ToTable("Review");

                entity.Property(e => e.ReviewId)
                    .ValueGeneratedNever()
                    .HasColumnName("review_id");

                entity.Property(e => e.Comment)
                    .IsUnicode(false)
                    .HasColumnName("comment");

                entity.Property(e => e.EntryDate)
                    .HasColumnType("datetime")
                    .HasColumnName("entry_date");

                entity.Property(e => e.Rating).HasColumnName("rating");

                entity.Property(e => e.UId).HasColumnName("u_id");

                entity.HasOne(d => d.UIdNavigation)
                    .WithMany(p => p.Reviews)
                    .HasForeignKey(d => d.UId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__Review__u_id__35BCFE0A");
            });

            modelBuilder.Entity<Room>(entity =>
            {
                entity.HasKey(e => new { e.RoomId, e.HotelId })
                    .HasName("PK__Room__6D38BD68688FC194");

                entity.ToTable("Room");

                entity.Property(e => e.RoomId).HasColumnName("room_id");

                entity.Property(e => e.HotelId).HasColumnName("hotel_id");

                entity.Property(e => e.Number).HasColumnName("number");

                entity.Property(e => e.Price)
                    .HasColumnType("decimal(8, 2)")
                    .HasColumnName("price");

                entity.Property(e => e.Size).HasColumnName("size");

                entity.HasOne(d => d.Hotel)
                    .WithMany(p => p.Rooms)
                    .HasForeignKey(d => d.HotelId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__Room__hotel_id__4E88ABD4");
            });

            modelBuilder.Entity<SightseeingPlace>(entity =>
            {
                entity.HasKey(e => e.PlaceId)
                    .HasName("PK__Sightsee__BF2B684A75DA6110");

                entity.ToTable("SightseeingPlace");

                entity.Property(e => e.PlaceId)
                    .ValueGeneratedNever()
                    .HasColumnName("place_id");

                entity.Property(e => e.EndTime)
                    .HasColumnType("datetime")
                    .HasColumnName("end_time");

                entity.Property(e => e.PlaceName)
                    .HasMaxLength(64)
                    .IsUnicode(false)
                    .HasColumnName("place_name");

                entity.Property(e => e.SDescription)
                    .IsUnicode(false)
                    .HasColumnName("s_description");

                entity.Property(e => e.SLocation)
                    .IsUnicode(false)
                    .HasColumnName("s_location");

                entity.Property(e => e.StartTime)
                    .HasColumnType("datetime")
                    .HasColumnName("start_time");
            });

            modelBuilder.Entity<Speak>(entity =>
            {
                entity.HasKey(e => new { e.UId, e.LanguageName })
                    .HasName("PK__speak__8CD3BFD20D1B90D9");

                entity.ToTable("speak");

                entity.Property(e => e.UId).HasColumnName("u_id");

                entity.Property(e => e.LanguageName)
                    .HasMaxLength(16)
                    .IsUnicode(false)
                    .HasColumnName("language_name");

                entity.HasOne(d => d.LanguageNameNavigation)
                    .WithMany(p => p.Speaks)
                    .HasForeignKey(d => d.LanguageName)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__speak__language___6477ECF3");

                entity.HasOne(d => d.UIdNavigation)
                    .WithMany(p => p.Speaks)
                    .HasForeignKey(d => d.UId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__speak__u_id__6383C8BA");
            });

            modelBuilder.Entity<Tour>(entity =>
            {
                entity.ToTable("Tour");

                entity.Property(e => e.TourId)
                    .ValueGeneratedNever()
                    .HasColumnName("tour_id");

                entity.Property(e => e.City)
                    .HasMaxLength(32)
                    .IsUnicode(false)
                    .HasColumnName("city");

                entity.Property(e => e.DiscountId).HasColumnName("discount_id");

                entity.Property(e => e.Price)
                    .HasColumnType("decimal(8, 2)")
                    .HasColumnName("price");

                entity.Property(e => e.TourDays).HasColumnName("tour_days");

                entity.Property(e => e.TourDescription)
                    .IsUnicode(false)
                    .HasColumnName("tour_description");

                entity.Property(e => e.TourName)
                    .HasMaxLength(64)
                    .IsUnicode(false)
                    .HasColumnName("tour_name");

                entity.Property(e => e.TourStartDate)
                    .HasColumnType("date")
                    .HasColumnName("tour_start_date");

                entity.HasOne(d => d.Discount)
                    .WithMany(p => p.Tours)
                    .HasForeignKey(d => d.DiscountId)
                    .HasConstraintName("FK__Tour__discount_i__25869641");
            });

            modelBuilder.Entity<TourReservation>(entity =>
            {
                entity.HasKey(e => e.ReserveId)
                    .HasName("PK__TourRese__BFAD1238BF55BD4F");

                entity.ToTable("TourReservation");

                entity.Property(e => e.ReserveId)
                    .ValueGeneratedNever()
                    .HasColumnName("reserve_id");

                entity.Property(e => e.TourId).HasColumnName("tour_id");

                entity.HasOne(d => d.Reserve)
                    .WithOne(p => p.TourReservation)
                    .HasForeignKey<TourReservation>(d => d.ReserveId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__TourReser__reser__5535A963");

                entity.HasOne(d => d.Tour)
                    .WithMany(p => p.TourReservations)
                    .HasForeignKey(d => d.TourId)
                    .HasConstraintName("FK__TourReser__tour___5441852A");
            });

            modelBuilder.Entity<TourReview>(entity =>
            {
                entity.HasKey(e => e.ReviewId)
                    .HasName("PK__TourRevi__60883D908A8F4599");

                entity.ToTable("TourReview");

                entity.Property(e => e.ReviewId)
                    .ValueGeneratedNever()
                    .HasColumnName("review_id");

                entity.Property(e => e.TourId).HasColumnName("tour_id");

                entity.HasOne(d => d.Review)
                    .WithOne(p => p.TourReview)
                    .HasForeignKey<TourReview>(d => d.ReviewId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__TourRevie__revie__3D5E1FD2");

                entity.HasOne(d => d.Tour)
                    .WithMany(p => p.TourReviews)
                    .HasForeignKey(d => d.TourId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__TourRevie__tour___3C69FB99");
            });

            modelBuilder.Entity<User>(entity =>
            {
                entity.HasKey(e => e.UId)
                    .HasName("PK__Users__B51D3DEAB4605DCE");

                entity.Property(e => e.UId)
                    .ValueGeneratedNever()
                    .HasColumnName("u_id");

                entity.Property(e => e.Age).HasColumnName("age");

                entity.Property(e => e.Email)
                    .HasMaxLength(255)
                    .IsUnicode(false)
                    .HasColumnName("email");

                entity.Property(e => e.FirstName)
                    .HasMaxLength(32)
                    .IsUnicode(false)
                    .HasColumnName("first_name");

                entity.Property(e => e.LastName)
                    .HasMaxLength(32)
                    .IsUnicode(false)
                    .HasColumnName("last_name");

                entity.Property(e => e.Pass)
                    .HasMaxLength(64)
                    .IsUnicode(false)
                    .HasColumnName("pass");

                entity.Property(e => e.PhoneNumber)
                    .HasMaxLength(15)
                    .IsUnicode(false)
                    .HasColumnName("phone_number");

                entity.Property(e => e.Username)
                    .HasMaxLength(32)
                    .IsUnicode(false)
                    .HasColumnName("username");
            });

            modelBuilder.Entity<Visit>(entity =>
            {
                entity.HasKey(e => new { e.PlaceId, e.TourId })
                    .HasName("PK__visits__CB9A03D4EFCC49EB");

                entity.ToTable("visits");

                entity.Property(e => e.PlaceId).HasColumnName("place_id");

                entity.Property(e => e.TourId).HasColumnName("tour_id");

                entity.HasOne(d => d.Place)
                    .WithMany(p => p.Visits)
                    .HasForeignKey(d => d.PlaceId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__visits__place_id__5FB337D6");

                entity.HasOne(d => d.Tour)
                    .WithMany(p => p.Visits)
                    .HasForeignKey(d => d.TourId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__visits__tour_id__60A75C0F");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
