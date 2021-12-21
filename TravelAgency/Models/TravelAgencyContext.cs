using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

// Code scaffolded by EF Core assumes nullable reference types (NRTs) are not used or disabled.
// If you have enabled NRTs for your project, then un-comment the following line:
// #nullable disable

namespace TravelAgency.Models
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

        public virtual DbSet<Activity> Activity { get; set; }
        public virtual DbSet<Agent> Agent { get; set; }
        public virtual DbSet<AssignGuide> AssignGuide { get; set; }
        public virtual DbSet<Concert> Concert { get; set; }
        public virtual DbSet<Customer> Customer { get; set; }
        public virtual DbSet<Discount> Discount { get; set; }
        public virtual DbSet<Employee> Employee { get; set; }
        public virtual DbSet<Feedback> Feedback { get; set; }
        public virtual DbSet<Festival> Festival { get; set; }
        public virtual DbSet<Guide> Guide { get; set; }
        public virtual DbSet<GuideReview> GuideReview { get; set; }
        public virtual DbSet<GuidesFeedback> GuidesFeedback { get; set; }
        public virtual DbSet<Hotel> Hotel { get; set; }
        public virtual DbSet<HotelReservation> HotelReservation { get; set; }
        public virtual DbSet<Languages> Languages { get; set; }
        public virtual DbSet<MarkedActivity> MarkedActivity { get; set; }
        public virtual DbSet<Reservation> Reservation { get; set; }
        public virtual DbSet<Review> Review { get; set; }
        public virtual DbSet<Room> Room { get; set; }
        public virtual DbSet<SightseeingPlace> SightseeingPlace { get; set; }
        public virtual DbSet<Speak> Speak { get; set; }
        public virtual DbSet<Tour> Tour { get; set; }
        public virtual DbSet<TourReservation> TourReservation { get; set; }
        public virtual DbSet<TourReview> TourReview { get; set; }
        public virtual DbSet<Users> Users { get; set; }
        public virtual DbSet<Visits> Visits { get; set; }

         /** 
          * Make sure the following packages are installed
          * Microsoft.EntityFrameworkCore 3.1.22
          * Microsoft.EntityFrameworkCore.SqlServer 3.1.22
          * Microsoft.EntityFrameworkCore.Tools 3.1.22
          */
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                // Connection string for the database.
                // Please change the data source parameter accordingly; the server you have 
                // exported the backup file. We could use a common online database,
                // but it would take time to setup.
                optionsBuilder.UseSqlServer("Data Source=(localdb)\\MSSQLLocalDB;Initial Catalog=TravelAgency;Integrated Security=True;MultipleActiveResultSets=True;");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Activity>(entity =>
            {
                entity.Property(e => e.ActivityId)
                    .HasColumnName("activity_id")
                    .ValueGeneratedNever();

                entity.Property(e => e.ADescription)
                    .HasColumnName("a_description")
                    .IsUnicode(false);

                entity.Property(e => e.ActivityEndTime)
                    .HasColumnName("activity_end_time")
                    .HasColumnType("datetime");

                entity.Property(e => e.ActivityName)
                    .HasColumnName("activity_name")
                    .HasMaxLength(64)
                    .IsUnicode(false);

                entity.Property(e => e.ActivityStartTime)
                    .HasColumnName("activity_start_time")
                    .HasColumnType("datetime");

                entity.Property(e => e.DiscountId).HasColumnName("discount_id");

                entity.Property(e => e.TicketPrice)
                    .HasColumnName("ticket_price")
                    .HasColumnType("decimal(8, 2)");

                entity.Property(e => e.TourId).HasColumnName("tour_id");

                entity.HasOne(d => d.Discount)
                    .WithMany(p => p.Activity)
                    .HasForeignKey(d => d.DiscountId)
                    .HasConstraintName("FK__Activity__discou__412EB0B6");

                entity.HasOne(d => d.Tour)
                    .WithMany(p => p.Activity)
                    .HasForeignKey(d => d.TourId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__Activity__tour_i__403A8C7D");
            });

            modelBuilder.Entity<Agent>(entity =>
            {
                entity.HasKey(e => e.UId)
                    .HasName("PK__Agent__B51D3DEAB9EC4CC5");

                entity.Property(e => e.UId)
                    .HasColumnName("u_id")
                    .ValueGeneratedNever();

                entity.HasOne(d => d.U)
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
                    .HasColumnName("tour_id")
                    .ValueGeneratedNever();

                entity.Property(e => e.AgentUId).HasColumnName("agent_u_id");

                entity.Property(e => e.AssignStatus)
                    .HasColumnName("assign_status")
                    .HasMaxLength(10)
                    .IsUnicode(false);

                entity.Property(e => e.GuideUId).HasColumnName("guide_u_id");

                entity.HasOne(d => d.AgentU)
                    .WithMany(p => p.AssignGuide)
                    .HasForeignKey(d => d.AgentUId)
                    .HasConstraintName("FK__assign_gu__agent__6C190EBB");

                entity.HasOne(d => d.GuideU)
                    .WithMany(p => p.AssignGuide)
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

                entity.Property(e => e.ActivityId)
                    .HasColumnName("activity_id")
                    .ValueGeneratedNever();

                entity.Property(e => e.ArtistName)
                    .HasColumnName("artist_name")
                    .IsUnicode(false);

                entity.Property(e => e.Genre)
                    .HasColumnName("genre")
                    .HasMaxLength(64)
                    .IsUnicode(false);

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

                entity.Property(e => e.UId)
                    .HasColumnName("u_id")
                    .ValueGeneratedNever();

                entity.Property(e => e.CAddress)
                    .HasColumnName("c_address")
                    .IsUnicode(false);

                entity.Property(e => e.Wallet)
                    .HasColumnName("wallet")
                    .HasColumnType("decimal(8, 2)");

                entity.HasOne(d => d.U)
                    .WithOne(p => p.Customer)
                    .HasForeignKey<Customer>(d => d.UId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__Customer__u_id__2A4B4B5E");
            });

            modelBuilder.Entity<Discount>(entity =>
            {
                entity.Property(e => e.DiscountId)
                    .HasColumnName("discount_id")
                    .ValueGeneratedNever();

                entity.Property(e => e.DiscountStartTime)
                    .HasColumnName("discount_start_time")
                    .HasColumnType("datetime");

                entity.Property(e => e.DiscountTimeInterval).HasColumnName("discount_time_interval");

                entity.Property(e => e.DiscountType)
                    .HasColumnName("discount_type")
                    .HasMaxLength(16)
                    .IsUnicode(false);

                entity.Property(e => e.Percents).HasColumnName("percents");
            });

            modelBuilder.Entity<Employee>(entity =>
            {
                entity.HasKey(e => e.UId)
                    .HasName("PK__Employee__B51D3DEACF4DADBC");

                entity.Property(e => e.UId)
                    .HasColumnName("u_id")
                    .ValueGeneratedNever();

                entity.Property(e => e.Salary)
                    .HasColumnName("salary")
                    .HasColumnType("decimal(8, 2)");

                entity.HasOne(d => d.U)
                    .WithOne(p => p.Employee)
                    .HasForeignKey<Employee>(d => d.UId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__Employee__u_id__2D27B809");
            });

            modelBuilder.Entity<Feedback>(entity =>
            {
                entity.Property(e => e.FeedbackId)
                    .HasColumnName("feedback_id")
                    .ValueGeneratedNever();

                entity.Property(e => e.Content)
                    .HasColumnName("content")
                    .IsUnicode(false);

                entity.Property(e => e.FeedbackTime)
                    .HasColumnName("feedback_time")
                    .HasColumnType("datetime");
            });

            modelBuilder.Entity<Festival>(entity =>
            {
                entity.HasKey(e => e.ActivityId)
                    .HasName("PK__Festival__482FBD63C98FDD05");

                entity.Property(e => e.ActivityId)
                    .HasColumnName("activity_id")
                    .ValueGeneratedNever();

                entity.Property(e => e.AgeLimit).HasColumnName("age_limit");

                entity.Property(e => e.FoodCatalog)
                    .HasColumnName("food_catalog")
                    .IsUnicode(false);

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

                entity.Property(e => e.UId)
                    .HasColumnName("u_id")
                    .ValueGeneratedNever();

                entity.HasOne(d => d.U)
                    .WithOne(p => p.Guide)
                    .HasForeignKey<Guide>(d => d.UId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__Guide__u_id__300424B4");
            });

            modelBuilder.Entity<GuideReview>(entity =>
            {
                entity.HasKey(e => e.ReviewId)
                    .HasName("PK__GuideRev__60883D90A47A9A42");

                entity.Property(e => e.ReviewId)
                    .HasColumnName("review_id")
                    .ValueGeneratedNever();

                entity.Property(e => e.UId).HasColumnName("u_id");

                entity.HasOne(d => d.Review)
                    .WithOne(p => p.GuideReview)
                    .HasForeignKey<GuideReview>(d => d.ReviewId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__GuideRevi__revie__398D8EEE");

                entity.HasOne(d => d.U)
                    .WithMany(p => p.GuideReview)
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
                    .HasColumnName("feedback_id")
                    .ValueGeneratedNever();

                entity.Property(e => e.TourId).HasColumnName("tour_id");

                entity.Property(e => e.UId).HasColumnName("u_id");

                entity.HasOne(d => d.Feedback)
                    .WithOne(p => p.GuidesFeedback)
                    .HasForeignKey<GuidesFeedback>(d => d.FeedbackId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__guides_fe__feedb__6754599E");

                entity.HasOne(d => d.Tour)
                    .WithMany(p => p.GuidesFeedback)
                    .HasForeignKey(d => d.TourId)
                    .HasConstraintName("FK__guides_fe__tour___693CA210");

                entity.HasOne(d => d.U)
                    .WithMany(p => p.GuidesFeedback)
                    .HasForeignKey(d => d.UId)
                    .HasConstraintName("FK__guides_fee__u_id__68487DD7");
            });

            modelBuilder.Entity<Hotel>(entity =>
            {
                entity.Property(e => e.HotelId)
                    .HasColumnName("hotel_id")
                    .ValueGeneratedNever();

                entity.Property(e => e.City)
                    .HasColumnName("city")
                    .HasMaxLength(32)
                    .IsUnicode(false);

                entity.Property(e => e.DiscountId).HasColumnName("discount_id");

                entity.Property(e => e.HotelName)
                    .HasColumnName("hotel_name")
                    .HasMaxLength(64)
                    .IsUnicode(false);

                entity.Property(e => e.NumOfStars).HasColumnName("num_of_stars");

                entity.HasOne(d => d.Discount)
                    .WithMany(p => p.Hotel)
                    .HasForeignKey(d => d.DiscountId)
                    .HasConstraintName("FK__Hotel__discount___4BAC3F29");
            });

            modelBuilder.Entity<HotelReservation>(entity =>
            {
                entity.HasKey(e => e.ReserveId)
                    .HasName("PK__HotelRes__BFAD1238893E9C3C");

                entity.Property(e => e.ReserveId)
                    .HasColumnName("reserve_id")
                    .ValueGeneratedNever();

                entity.Property(e => e.HotelId).HasColumnName("hotel_id");

                entity.Property(e => e.ReserveEndDate)
                    .HasColumnName("reserve_end_date")
                    .HasColumnType("date");

                entity.Property(e => e.RoomId).HasColumnName("room_id");

                entity.HasOne(d => d.Reserve)
                    .WithOne(p => p.HotelReservation)
                    .HasForeignKey<HotelReservation>(d => d.ReserveId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__HotelRese__reser__59063A47");

                entity.HasOne(d => d.Room)
                    .WithMany(p => p.HotelReservation)
                    .HasForeignKey(d => new { d.RoomId, d.HotelId })
                    .HasConstraintName("FK__HotelReservation__5812160E");
            });

            modelBuilder.Entity<Languages>(entity =>
            {
                entity.HasKey(e => e.LanguageName)
                    .HasName("PK__Language__9CE8238201D46FC2");

                entity.Property(e => e.LanguageName)
                    .HasColumnName("language_name")
                    .HasMaxLength(16)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<MarkedActivity>(entity =>
            {
                entity.HasKey(e => new { e.ReserveId, e.ActivityId })
                    .HasName("PK__marked_a__9B2FE9EE0F25E44B");

                entity.ToTable("marked_activity");

                entity.Property(e => e.ReserveId).HasColumnName("reserve_id");

                entity.Property(e => e.ActivityId).HasColumnName("activity_id");

                entity.HasOne(d => d.Activity)
                    .WithMany(p => p.MarkedActivity)
                    .HasForeignKey(d => d.ActivityId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__marked_ac__activ__71D1E811");

                entity.HasOne(d => d.Reserve)
                    .WithMany(p => p.MarkedActivity)
                    .HasForeignKey(d => d.ReserveId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__marked_ac__reser__70DDC3D8");
            });

            modelBuilder.Entity<Reservation>(entity =>
            {
                entity.HasKey(e => e.ReserveId)
                    .HasName("PK__Reservat__BFAD1238E25227DF");

                entity.Property(e => e.ReserveId)
                    .HasColumnName("reserve_id")
                    .ValueGeneratedNever();

                entity.Property(e => e.IsBooked).HasColumnName("is_booked");

                entity.Property(e => e.NumReserving).HasColumnName("num_reserving");

                entity.Property(e => e.ReserveStartDate)
                    .HasColumnName("reserve_start_date")
                    .HasColumnType("date");

                entity.Property(e => e.UId).HasColumnName("u_id");

                entity.HasOne(d => d.U)
                    .WithMany(p => p.Reservation)
                    .HasForeignKey(d => d.UId)
                    .HasConstraintName("FK__Reservatio__u_id__5165187F");
            });

            modelBuilder.Entity<Review>(entity =>
            {
                entity.Property(e => e.ReviewId)
                    .HasColumnName("review_id")
                    .ValueGeneratedNever();

                entity.Property(e => e.Comment)
                    .HasColumnName("comment")
                    .IsUnicode(false);

                entity.Property(e => e.EntryDate)
                    .HasColumnName("entry_date")
                    .HasColumnType("datetime");

                entity.Property(e => e.Rating).HasColumnName("rating");

                entity.Property(e => e.UId).HasColumnName("u_id");

                entity.HasOne(d => d.U)
                    .WithMany(p => p.Review)
                    .HasForeignKey(d => d.UId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__Review__u_id__35BCFE0A");
            });

            modelBuilder.Entity<Room>(entity =>
            {
                entity.HasKey(e => new { e.RoomId, e.HotelId })
                    .HasName("PK__Room__6D38BD68688FC194");

                entity.Property(e => e.RoomId).HasColumnName("room_id");

                entity.Property(e => e.HotelId).HasColumnName("hotel_id");

                entity.Property(e => e.Number).HasColumnName("number");

                entity.Property(e => e.Price)
                    .HasColumnName("price")
                    .HasColumnType("decimal(8, 2)");

                entity.Property(e => e.Size).HasColumnName("size");

                entity.HasOne(d => d.Hotel)
                    .WithMany(p => p.Room)
                    .HasForeignKey(d => d.HotelId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__Room__hotel_id__4E88ABD4");
            });

            modelBuilder.Entity<SightseeingPlace>(entity =>
            {
                entity.HasKey(e => e.PlaceId)
                    .HasName("PK__Sightsee__BF2B684A75DA6110");

                entity.Property(e => e.PlaceId)
                    .HasColumnName("place_id")
                    .ValueGeneratedNever();

                entity.Property(e => e.EndTime)
                    .HasColumnName("end_time")
                    .HasColumnType("datetime");

                entity.Property(e => e.PlaceName)
                    .HasColumnName("place_name")
                    .HasMaxLength(64)
                    .IsUnicode(false);

                entity.Property(e => e.SDescription)
                    .HasColumnName("s_description")
                    .IsUnicode(false);

                entity.Property(e => e.SLocation)
                    .HasColumnName("s_location")
                    .IsUnicode(false);

                entity.Property(e => e.StartTime)
                    .HasColumnName("start_time")
                    .HasColumnType("datetime");
            });

            modelBuilder.Entity<Speak>(entity =>
            {
                entity.HasKey(e => new { e.UId, e.LanguageName })
                    .HasName("PK__speak__8CD3BFD20D1B90D9");

                entity.ToTable("speak");

                entity.Property(e => e.UId).HasColumnName("u_id");

                entity.Property(e => e.LanguageName)
                    .HasColumnName("language_name")
                    .HasMaxLength(16)
                    .IsUnicode(false);

                entity.HasOne(d => d.LanguageNameNavigation)
                    .WithMany(p => p.Speak)
                    .HasForeignKey(d => d.LanguageName)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__speak__language___6477ECF3");

                entity.HasOne(d => d.U)
                    .WithMany(p => p.Speak)
                    .HasForeignKey(d => d.UId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__speak__u_id__6383C8BA");
            });

            modelBuilder.Entity<Tour>(entity =>
            {
                entity.Property(e => e.TourId)
                    .HasColumnName("tour_id")
                    .ValueGeneratedNever();

                entity.Property(e => e.City)
                    .HasColumnName("city")
                    .HasMaxLength(32)
                    .IsUnicode(false);

                entity.Property(e => e.DiscountId).HasColumnName("discount_id");

                entity.Property(e => e.Price)
                    .HasColumnName("price")
                    .HasColumnType("decimal(8, 2)");

                entity.Property(e => e.TourDays).HasColumnName("tour_days");

                entity.Property(e => e.TourDescription)
                    .HasColumnName("tour_description")
                    .IsUnicode(false);

                entity.Property(e => e.TourName)
                    .HasColumnName("tour_name")
                    .HasMaxLength(64)
                    .IsUnicode(false);

                entity.Property(e => e.TourStartDate)
                    .HasColumnName("tour_start_date")
                    .HasColumnType("date");

                entity.HasOne(d => d.Discount)
                    .WithMany(p => p.Tour)
                    .HasForeignKey(d => d.DiscountId)
                    .HasConstraintName("FK__Tour__discount_i__25869641");
            });

            modelBuilder.Entity<TourReservation>(entity =>
            {
                entity.HasKey(e => e.ReserveId)
                    .HasName("PK__TourRese__BFAD1238BF55BD4F");

                entity.Property(e => e.ReserveId)
                    .HasColumnName("reserve_id")
                    .ValueGeneratedNever();

                entity.Property(e => e.TourId).HasColumnName("tour_id");

                entity.HasOne(d => d.Reserve)
                    .WithOne(p => p.TourReservation)
                    .HasForeignKey<TourReservation>(d => d.ReserveId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__TourReser__reser__5535A963");

                entity.HasOne(d => d.Tour)
                    .WithMany(p => p.TourReservation)
                    .HasForeignKey(d => d.TourId)
                    .HasConstraintName("FK__TourReser__tour___5441852A");
            });

            modelBuilder.Entity<TourReview>(entity =>
            {
                entity.HasKey(e => e.ReviewId)
                    .HasName("PK__TourRevi__60883D908A8F4599");

                entity.Property(e => e.ReviewId)
                    .HasColumnName("review_id")
                    .ValueGeneratedNever();

                entity.Property(e => e.TourId).HasColumnName("tour_id");

                entity.HasOne(d => d.Review)
                    .WithOne(p => p.TourReview)
                    .HasForeignKey<TourReview>(d => d.ReviewId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__TourRevie__revie__3D5E1FD2");

                entity.HasOne(d => d.Tour)
                    .WithMany(p => p.TourReview)
                    .HasForeignKey(d => d.TourId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__TourRevie__tour___3C69FB99");
            });

            modelBuilder.Entity<Users>(entity =>
            {
                entity.HasKey(e => e.UId)
                    .HasName("PK__Users__B51D3DEAB4605DCE");

                entity.Property(e => e.UId)
                    .HasColumnName("u_id")
                    .ValueGeneratedNever();

                entity.Property(e => e.Age).HasColumnName("age");

                entity.Property(e => e.Email)
                    .HasColumnName("email")
                    .HasMaxLength(255)
                    .IsUnicode(false);

                entity.Property(e => e.FirstName)
                    .HasColumnName("first_name")
                    .HasMaxLength(32)
                    .IsUnicode(false);

                entity.Property(e => e.LastName)
                    .HasColumnName("last_name")
                    .HasMaxLength(32)
                    .IsUnicode(false);

                entity.Property(e => e.Pass)
                    .HasColumnName("pass")
                    .HasMaxLength(64)
                    .IsUnicode(false);

                entity.Property(e => e.PhoneNumber)
                    .HasColumnName("phone_number")
                    .HasMaxLength(15)
                    .IsUnicode(false);

                entity.Property(e => e.Username)
                    .HasColumnName("username")
                    .HasMaxLength(32)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<Visits>(entity =>
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
