<%@ Page Title="Government Expenses in Canada" Language="C#" MasterPageFile="~/default.master" AutoEventWireup="True" CodeBehind="Default.aspx.cs" Inherits="Metamorph._Default" %>
<%@ Register src="top_spenders.ascx" tagname="top_spenders" tagprefix="uc1" %>

<%@ Register src="department_changes.ascx" tagname="department_changes" tagprefix="uc2" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

	<br />
	
    <div class="intro">
   

   <h2>Getting Tough on Government Expenses</h2>

   <h2>Get ready for some exciting new updates, coming to you via the <a href="http://www.taxpayer.com">Canadian Taxpayer's Federation</a> in the fall!</h2>

   <h2><font color="Maroon">Look at these results!</font></h2>  
   Millions of dollars have been saved on expenses from the year before
   I completed the project compared to the year after it went public.  The expense graphs
   under each department
   show a clear picture.  Continually rising expenses until suddenly in 2010 a dramatic drop
   across almost all of the departments!


	<h3>Largest Spending Cutbacks from 2009 to 2010</h3>
	<b>These departments need to be congratulated for cutting costs and deserve a
	sincere THANK YOU from the Canadian taxpayer for saving their hard earned dollars!</b>
	<br /><br />
	<uc2:department_changes ID="ctlDepartmentDecreases" runat="server" />
	
	<br /><br />
   

   This is what one person can do by himself, with a zero dollar budget and no official
   access to the government data other than what was proactively disclosed on the tangled
   websites.  Just imagine what I'd be capable of if you gave me
   money, staff, and official clearance.  
   <br /><br />
   
   <b>Think of how many social programs can be funded with all that saved money!</b>
   
   <br /><br />
   The next time you're waiting for health care, that wait
   time might be a little shorter; the next time you or your child seeks out education,
   it might be a little less costly for you and be of a little bit higher quality;
   the next time you seek out any public service from your government, you should expect
   it to be just that much quicker and of a higher calibre than usual because money and
   resources were saved by sensible, efficient government staff.

   <br /><br />

   <br />

   <b>Data Last Retrieved: January 10, 2011</b>

   <br /><br />

    A window into the Canadian federal government expenses.  No longer can your
    public servants hide behind layers of obscurity.  Part of the 
    <a href="http://equilism.info/">equilism</a>
    project, a multi-generational vision of positive social change.  This
    is all done pro-bono with no funding or resources other than what is 
    <a href="/donate.aspx">donated</a>
    by good samaritans.

   <br /><br />

   This project was completed primarily by a single person in approximately
   500 man-hours.  At a contractor rate of $50/hr, it would have cost $25,000.
   The next time government does an IT / database project and claims to need 
   millions or even billions of dollars, keep in mind what can be done on a 
   five figure budget.

	</div>

        <br /><br />

    
	<a name="reports"></a>	

    <div class="bigtext">
    Since 2003 we have assessed a total of 
    <asp:Label ID="lblCount" runat="server" Font-Bold="true"></asp:Label> 
    expenses claimed
    with the total expense bill running exactly 
    <asp:label id="lblTotal" runat="server" Font-Bold="true" />
    and is growing!
    </div>

	<br />

	<h3>Largest Spending Increases from 2009 to 2010</h3>
	<b>Where is the world going and why is it in a handbasket?  Thank you Public Safety and
	Emergency Preparedness Canada for getting us all ready for 2012!</b>
	<br /><br />
	<uc2:department_changes ID="ctlDepartmentIncreases" runat="server" />

	<br /><br />

	<h3>Department Spending Trends</h3>

	<asp:Repeater id="ctlDepartments" runat="server">
		<HeaderTemplate>
			<table width="100%" cellspacing="5">
				<tr>
					<td><b>Department</b></td>
					<td><b>Total Spent</b></td>
					<td><b>Number of Expenses</b></td>
					<td><b>Number of Employees</b></td>
				</tr>
		</HeaderTemplate>
		<ItemTemplate>
			<tr>
				<td><a href="departments.aspx?id=<%# DataBinder.Eval(Container.DataItem, "department_id") %>&department=<%# HttpUtility.UrlEncode(DataBinder.Eval(Container.DataItem, "department_name").ToString()) %>"><%# DataBinder.Eval(Container.DataItem, "department_name") %></a></td>
				<td><%# ((decimal)DataBinder.Eval(Container.DataItem, "total")).ToString("C") %></td>
				<td><%# DataBinder.Eval(Container.DataItem, "count") %></td>
				<td><%# DataBinder.Eval(Container.DataItem, "member_count") %></td>
			</tr>
		</ItemTemplate>
		<SeparatorTemplate></SeparatorTemplate>
		<FooterTemplate></table></FooterTemplate>
	</asp:Repeater>
	
	<br /><br />
	
	<h3>Top Spenders Overall 2003-Present</h3>

	<uc1:top_spenders ID="ctlTopSpenders" runat="server" />
	
	<br />
	
	<h3>Top Spenders For Each Year</h3>
	
	<h4>2010</h4>
	<uc1:top_spenders ID="ctlTopSpendersForYear2010" runat="server" Year="2010" />

	<h4>2009</h4>
	<uc1:top_spenders ID="ctlTopSpendersForYear2009" runat="server" Year="2009" />

	<h4>2008</h4>
	<uc1:top_spenders ID="ctlTopSpendersForYear2008" runat="server" Year="2008" />

	<h4>2007</h4>
	<uc1:top_spenders ID="ctlTopSpendersForYear2007" runat="server" Year="2007" />

	<h4>2006</h4>
	<uc1:top_spenders ID="ctlTopSpendersForYear2006" runat="server" Year="2006" />

	<h4>2005</h4>
	<uc1:top_spenders ID="ctlTopSpendersForYear2005" runat="server" Year="2005" />

	<h4>2004</h4>
	<uc1:top_spenders ID="ctlTopSpendersForYear2004" runat="server" />

	<br /><br />

	<h3>Updates</h3>

   	<b>UPDATE [2011-01-10]:</b> The new data set is up and available.  I'm going to do a re-crawl
	in the next few days to grab a couple of fixes.  In the meantime, custom reports are now 
	<b>FREE</b>	to all.  I'm going to create a change report to highlight the biggest increases
	and decreases in spending between 2009 and 2010 to see if this project had any effect.
	
	<br /><br />

	<b>UPDATE [2011-01-06]:</b> I am making progress on updating the crawler and plan to release
	the data within the next few days if all goes according to plan.  On that date, 
	I will enable public access to the expense search tool for the day.

	<br /><br />

	<b>UPDATE [2011-01-05]:</b> The first full run of the crawler across all sites has generated
	40 megs of logfiles, so now it's a matter of going through them with a fine-toothed comb to
	see what can be fixed, test each of the individual fixes and eventually do another crawler run
	to get the final version of the data.

	<br /><br />
	
	<b>UPDATE [2010-12-30]:</b> As promised, I am starting a run of the government expenses
	crawler engine.  Let's hope the first run goes well and there aren't too many errors to
	correct.  Also, as part of my protest against greed and over-consumption, while most people
	are gorging themselves with turkey and pies and cookies, I have been fasting since christmas.
	I do this for health purposes and to draw a line of contrast between the current greedy
	state of the world and a necessary counterbalance to repair a mindset borne out of survival
	but taken to extreme due to affluence.  Wish me luck!
	<br /><br />

	<b>UPDATE [2010-12-08]:</b> The current data set was downloaded in the summer of 2010.
	I am hoping to get a new version downloaded this winter, provided I can
	find time to update the crawler to match the current government site format.
	If anyone has any interest in doing a story about the latest results,
	please <a href="mailto:govexp@bine.ca">let me know</a>.
	<br /><br />

    In the upcoming quarters, we will be looking for a broad spectrum reduction
	in government expense claims.  Any departments or individuals who do not
	comply with this mandate will be put into the spotlight to try and explain
	their actions.

	<br /><br />
	
	Special recognition in accommodating taxpayers in these tough economic
	times will go to the departments which demonstrate
	the most effective cost savings in the upcoming quarters and years.

	<br /><br />
	We will also be looking
	to have the full MP expenses which are hidden in the "Other" category released
	to the public.  The moment legislation is passed to release these expenses,
	this site will pick them up and publish reports for the entire country
	to see.
	<br /><br />


</asp:Content>
