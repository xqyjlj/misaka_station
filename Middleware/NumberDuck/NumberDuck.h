// Copyright (C) 2013-2020 File Scribe
// All Rights Reserved.
//
// Use of this code is covered by the "NUMBER DUCK API LICENSE AGREEMENT".
// Details of this agreement are found in the file "licence.txt".
// You have permission to modify this file and use it for any purpose under the terms of
// the agreement.

#ifndef NUMBER_DUCK_H
#define NUMBER_DUCK_H
	#if defined(_WIN32) || defined(__WIN32__)
		#if defined(NUMBER_DUCK_EXPORT)
			#define ND_DLL __declspec(dllexport)
		#elif defined(NUMBER_DUCK_STATIC)
			#define ND_DLL
		#else
			#define ND_DLL __declspec(dllimport)
		#endif
	#else
		#define ND_DLL
	#endif

	#include <stdint.h>
	namespace NumberDuck
	{
		class Workbook;
		class Worksheet;
		class Cell;
		class Value;
		class Picture;
		class Chart;
		class Series;
		class Legend;
		class Style;
		class Color;
		class Font;
		class Line;
		class Fill;
		class Marker;
		class MergedCell;
		
		
		// ****************************************************************************
		// Workbook
		// ****************************************************************************
			class Workbook
			{
				public:
					ND_DLL Workbook(const char* szRegistrationKey);
					ND_DLL ~Workbook();

					ND_DLL void Clear();

					ND_DLL bool Load(const char * szFileName);
					ND_DLL bool Save(const char * szFileName);
					
					ND_DLL uint16_t GetNumWorksheet();
					ND_DLL Worksheet* GetWorksheetByIndex(uint16_t nIndex);
					ND_DLL Worksheet* CreateWorksheet();
					ND_DLL void PurgeWorksheet(uint16_t nIndex);

					ND_DLL uint16_t GetNumStyle();
					ND_DLL Style* GetStyleByIndex(uint16_t nIndex);
					ND_DLL Style* GetDefaultStyle();
					ND_DLL Style* CreateStyle();

				private:
					class Implementation;
					Implementation* m_pImpl;
			};
		
		// ****************************************************************************
		// Cell
		// ****************************************************************************
			class ValueImplementation;
			class Value
			{
				public:
					enum Type
					{
						TYPE_EMPTY,
						TYPE_STRING,
						TYPE_FLOAT,
						TYPE_BOOLEAN,
						TYPE_FORMULA,
						TYPE_AREA,
						TYPE_AREA_3D,
						TYPE_ERROR
					};

					ND_DLL bool Equals(const Value* pValue) const;

					ND_DLL Type GetType() const;

					ND_DLL const char* GetString() const;
					ND_DLL double GetFloat() const;
					ND_DLL bool GetBoolean() const;
					ND_DLL const char* GetFormula() const;
					ND_DLL const Value* EvaulateFormula() const;

				private:
					friend class ValueImplementation;
					friend class Cell;
					friend class CellImplementation;
					friend class ChartImplementation;
					friend class Series;
					friend class SumToken;

					Value();
					~Value();
					ValueImplementation* m_pImpl;
			};
			
			class CellImplementation;
			class Cell
			{
				public:
					ND_DLL bool Equals(const Cell* pCell) const;

					ND_DLL const Value* GetValue();
					ND_DLL Value::Type GetType();
					ND_DLL void Clear();

					ND_DLL void SetString(const char* szString);
					ND_DLL const char* GetString();

					ND_DLL void SetFloat(double fFloat);
					ND_DLL double GetFloat();

					ND_DLL void SetBoolean(bool bBoolean);
					ND_DLL bool GetBoolean();

					ND_DLL void SetFormula(const char* szFormula);
					ND_DLL const char* GetFormula();
					ND_DLL const Value* EvaulateFormula() const;

					ND_DLL bool SetStyle(Style* pStyle);
					ND_DLL Style* GetStyle();

				protected:
					friend class Worksheet;
					friend class BiffWorksheet;
					Cell(Worksheet* pWorksheet);
					~Cell();

					CellImplementation* m_pImpl;
			};
			
			
		// ****************************************************************************
		// Picture
		// ****************************************************************************
			class Picture
			{
				public:
					enum Format
					{
						FORMAT_PNG,
						FORMAT_JPEG, // FORMAT_JPEG_CMYK,
						FORMAT_EMF,
						FORMAT_WMF,
						FORMAT_PICT,
						FORMAT_DIB,
						FORMAT_TIFF,
					};

					ND_DLL uint16_t GetX();
					ND_DLL bool SetX(uint16_t nX);

					ND_DLL uint16_t GetY();
					ND_DLL bool SetY(uint16_t nY);

					ND_DLL uint16_t GetSubX();
					ND_DLL void SetSubX(uint16_t nSubX);

					ND_DLL uint16_t GetSubY();
					ND_DLL void SetSubY(uint16_t nSubY);

					ND_DLL uint16_t GetWidth();
					ND_DLL void SetWidth(uint16_t nWidth);

					ND_DLL uint16_t GetHeight();
					ND_DLL void SetHeight(uint16_t nHeight);

					ND_DLL const char* GetUrl();
					ND_DLL void SetUrl(const char* szUrl);

					ND_DLL const uint8_t* GetData();
					ND_DLL uint32_t GetDataSize();
					ND_DLL Format GetFormat();

				private:
					friend class Worksheet;
					friend class BiffWorksheet;
					Picture(uint8_t* pData, uint32_t nDataSize, Format eFormat, Worksheet* pWorksheet);
					virtual ~Picture();

					class Implementation;
					Implementation* m_pImplementation;
			};
			
			
			
		
		// ****************************************************************************
		// Chart
		// ****************************************************************************
			class ChartImplementation;
			class Chart
			{
				public:
					enum Type
					{
						TYPE_COLUMN = 0,
						TYPE_COLUMN_STACKED,
						TYPE_COLUMN_STACKED_100,
						TYPE_BAR,
						TYPE_BAR_STACKED,
						TYPE_BAR_STACKED_100,
						TYPE_LINE,
						TYPE_LINE_STACKED,
						TYPE_LINE_STACKED_100,
						TYPE_AREA,
						TYPE_AREA_STACKED,
						TYPE_AREA_STACKED_100,
						//TYPE_PIE,
						//TYPE_PIE_SEPERATED,
						TYPE_SCATTER,
						NUM_TYPE
					};
					
					
					ND_DLL uint16_t GetX();
					ND_DLL bool SetX(uint16_t nX);

					ND_DLL uint16_t GetY();
					ND_DLL bool SetY(uint16_t nY);

					ND_DLL uint16_t GetSubX();
					ND_DLL void SetSubX(uint16_t nSubX);

					ND_DLL uint16_t GetSubY();
					ND_DLL void SetSubY(uint16_t nSubY);

					ND_DLL uint16_t GetWidth();
					ND_DLL void SetWidth(uint16_t nWidth);

					ND_DLL uint16_t GetHeight();
					ND_DLL void SetHeight(uint16_t nHeight);
					
					ND_DLL Type GetType();
					ND_DLL void SetType(Type eType);

					ND_DLL uint16_t GetNumSeries();
					ND_DLL Series* GetSeriesByIndex(uint16_t nIndex);
					ND_DLL Series* CreateSeries(const char* szValues); // eg: =A1:L1 , =Sheet1!$A$2:$L$2
					ND_DLL void PurgeSeries(uint16_t nIndex);
					
					ND_DLL const char* GetCategories();
					ND_DLL bool SetCategories(const char* szCategories); // Eg: "=Sheet1!$A$1:$L$1"
					
					ND_DLL const char* GetTitle();
					ND_DLL void SetTitle(const char* szTitle);
					
					ND_DLL const char* GetHorizontalAxisLabel();
					ND_DLL void SetHorizontalAxisLabel(const char* szHorizontalAxisLabel);
					
					ND_DLL const char* GetVerticalAxisLabel();
					ND_DLL void SetVerticalAxisLabel(const char* szVerticalAxisLabel);
					
					ND_DLL Legend* GetLegend();
					
					// Pretties
						ND_DLL Line* GetFrameBorderLine();
						ND_DLL Fill* GetFrameFill();
						
						ND_DLL Line* GetPlotBorderLine();
						ND_DLL Fill* GetPlotFill();
						
						ND_DLL Line* GetHorizontalAxisLine();
						ND_DLL Line* GetHorizontalGridLine();
						ND_DLL Line* GetVerticalAxisLine();
						ND_DLL Line* GetVerticalGridLine();
					
				private:
					friend class Worksheet;
					friend class BiffWorksheet;
					
					Chart(Worksheet* pWorksheet, Type eType);
					virtual ~Chart();

					ChartImplementation* m_pImpl;
			};
			
			class Formula;
			class SeriesImplementation;
			class Series
			{
				public:
					ND_DLL const char* GetName();
					ND_DLL bool SetName(const char* szName); // Eg: "Cars Sold" or "=Sheet1!A3" or "=\"Cars Sold\""
					
					ND_DLL const char* GetValues();
					ND_DLL bool SetValues(const char* szValues); // Eg: "={1,2,3,4}" or "=Sheet1!$A$2:$E$2"
					
					ND_DLL Line* GetLine();
					ND_DLL Fill* GetFill();
					ND_DLL Marker* GetMarker();

				private:
					friend class Chart;
					friend class ChartImplementation;
					friend class BiffWorksheet;
					
					Series(Worksheet* pWorksheet, Formula* pValuesFormula);
					~Series();

					SeriesImplementation* m_pImpl;
			};
			
			class LegendImplementation;
			class Legend
			{
				public:
					ND_DLL bool GetHidden() const;
					ND_DLL void SetHidden(bool bHidden);
					
					ND_DLL Line* GetBorderLine();
					ND_DLL Fill* GetFill();
					
				private:
					friend class Chart;
					friend class ChartImplementation;
					
					Legend();
					~Legend();

					LegendImplementation* m_pImpl;
			};
		
		
		// ****************************************************************************
		// Worksheet
		// ****************************************************************************
			class WorksheetImplementation;
			class Worksheet
			{
				public:
					static const uint16_t MAX_COLUMN = 255;
					static const uint16_t MAX_ROW = 65535;
					static const uint16_t DEFAULT_COLUMN_WIDTH = 64;
					static const uint16_t DEFAULT_ROW_HEIGHT = 20;

					enum Orientation
					{
						ORIENTATION_PORTRAIT,
						ORIENTATION_LANDSCAPE
					};

					ND_DLL const char* GetName();
					ND_DLL bool SetName(const char* szName);

					ND_DLL Orientation GetOrientation();
					ND_DLL void SetOrientation(Orientation eOrientation);

					ND_DLL bool GetPrintGridlines();
					ND_DLL void SetPrintGridlines(bool bPrintGridlines);

					ND_DLL bool GetShowGridlines();
					ND_DLL void SetShowGridlines(bool bShowGridlines);

					ND_DLL uint16_t GetColumnWidth(uint16_t nColumn);
					ND_DLL void SetColumnWidth(uint16_t nColumn, uint16_t nWidth);
					ND_DLL bool GetColumnHidden(uint16_t nColumn);
					ND_DLL void SetColumnHidden(uint16_t nColumn, bool bHidden);
					ND_DLL void InsertColumn(uint16_t nColumn);
					ND_DLL void DeleteColumn(uint16_t nColumn);

					ND_DLL uint16_t GetRowHeight(uint16_t nRow);
					ND_DLL void SetRowHeight(uint16_t nRow, uint16_t nHeight);
					ND_DLL void InsertRow(uint16_t nRow);
					ND_DLL void DeleteRow(uint16_t nRow);

					ND_DLL Cell* GetCell(uint16_t nX, uint16_t nY);
					ND_DLL Cell* GetCellByRC(uint16_t nRow, uint16_t nColumn);
					ND_DLL Cell* GetCellByAddress(const char* szAddress);

					ND_DLL uint16_t GetNumPicture();
					ND_DLL Picture* GetPictureByIndex(uint16_t nIndex);
					ND_DLL Picture* CreatePicture(const char* szFileName);
					ND_DLL void PurgePicture(uint16_t nIndex);

					ND_DLL uint16_t GetNumChart();
					ND_DLL Chart* GetChartByIndex(uint16_t nIndex);
					ND_DLL Chart* CreateChart(Chart::Type eType);
					ND_DLL void PurgeChart(uint16_t nIndex);

					ND_DLL uint16_t GetNumMergedCell();
					ND_DLL MergedCell* GetMergedCellByIndex(uint16_t nIndex);
					ND_DLL MergedCell* CreateMergedCell(uint16_t nX, uint16_t nY, uint16_t nWidth, uint16_t nHeight);
					ND_DLL void PurgeMergedCell(uint16_t nIndex);
					
				protected:
					friend class Workbook;
					friend class Value;
					friend class ValueImplementation;
					friend class Cell;
					friend class CellImplementation;
					friend class Chart;
					friend class Series;
					friend class Formula;
					friend class SumToken;
					friend class ChartImplementation;

					friend class BiffWorksheet;

					Worksheet(Workbook* pWorkbook);
					virtual ~Worksheet();
					WorksheetImplementation* m_pImpl;
			};
		
		// ****************************************************************************
		// Style
		// ****************************************************************************
			class WorkbookGlobals;
			class StyleImplementation;
			class Style
			{
				public:
					ND_DLL Font* GetFont();
					
					enum HorizontalAlign
					{
						HORIZONTAL_ALIGN_GENERAL = 0,
						HORIZONTAL_ALIGN_LEFT,
						HORIZONTAL_ALIGN_CENTER,
						HORIZONTAL_ALIGN_RIGHT,
						HORIZONTAL_ALIGN_FILL,
						HORIZONTAL_ALIGN_JUSTIFY,
						HORIZONTAL_ALIGN_CENTER_ACROSS_SELECTION,
						HORIZONTAL_ALIGN_DISTRIBUTED,
						NUM_HORIZONTAL_ALIGN
					};
					ND_DLL HorizontalAlign GetHorizontalAlign();
					ND_DLL void SetHorizontalAlign(HorizontalAlign eHorizontalAlign);
					
					enum VerticalAlign
					{
						VERTICAL_ALIGN_TOP = 0,
						VERTICAL_ALIGN_CENTER,
						VERTICAL_ALIGN_BOTTOM,
						VERTICAL_ALIGN_JUSTIFY,
						VERTICAL_ALIGN_DISTRIBUTED,
						NUM_VERTICAL_ALIGN
					};
					ND_DLL VerticalAlign GetVerticalAlign();
					ND_DLL void SetVerticalAlign(VerticalAlign eVerticalAlign);
					
					ND_DLL const Color* GetBackgroundColor();
					ND_DLL void SetBackgroundColor(const Color& color);
					ND_DLL void ClearBackgroundColor();

					enum FillPattern
					{
						FILL_PATTERN_NONE = 0,
						FILL_PATTERN_50, // 50% fill
						FILL_PATTERN_75, // 75% fill
						FILL_PATTERN_25, // 25% fill
						FILL_PATTERN_125, // 12.5% fill
						FILL_PATTERN_625, // 6.25% fill
						FILL_PATTERN_HORIZONTAL_STRIPE,
						FILL_PATTERN_VARTICAL_STRIPE,
						FILL_PATTERN_DIAGONAL_STRIPE,
						FILL_PATTERN_REVERSE_DIAGONAL_STRIPE,
						FILL_PATTERN_DIAGONAL_CROSSHATCH,
						FILL_PATTERN_THICK_DIAGONAL_CROSSHATCH,
						FILL_PATTERN_THIN_HORIZONTAL_STRIPE,
						FILL_PATTERN_THIN_VERTICAL_STRIPE,
						FILL_PATTERN_THIN_REVERSE_VERTICAL_STRIPE,
						FILL_PATTERN_THIN_DIAGONAL_STRIPE,
						FILL_PATTERN_THIN_HORIZONTAL_CROSSHATCH,
						FILL_PATTERN_THIN_DIAGONAL_CROSSHATCH,
						NUM_FILL_PATTERN
					};

					ND_DLL FillPattern GetFillPattern();
					ND_DLL void SetFillPattern(FillPattern eFillPattern);

					ND_DLL const Color* GetFillPatternColor();
					ND_DLL void SetFillPatternColor(const Color& color);
					ND_DLL void ClearFillPatternColor();
					
					ND_DLL Line* GetTopBorderLine();
					ND_DLL Line* GetRightBorderLine();
					ND_DLL Line* GetBottomBorderLine();
					ND_DLL Line* GetLeftBorderLine();

					ND_DLL const char* GetFormat();
					ND_DLL void SetFormat(const char* szFormat);
				private:
					friend class WorkbookGlobals;
					friend class BiffWorkbookGlobals;
					friend class XlsxWorkbookGlobals;
					Style(WorkbookGlobals* pWorkbookGlobals);
					virtual ~Style();

					
					StyleImplementation* m_pImplementation;
			};
			
			class Color
			{
				public:
					ND_DLL Color(uint8_t nRed, uint8_t nGreen, uint8_t nBlue);
					ND_DLL bool Equals(const Color* pColor) const;

					ND_DLL uint8_t GetRed() const;
					ND_DLL uint8_t GetGreen() const;
					ND_DLL uint8_t GetBlue() const;

					ND_DLL void SetRed(uint8_t nRed);
					ND_DLL void SetGreen(uint8_t nGreen);
					ND_DLL void SetBlue(uint8_t nBlue);

					ND_DLL uint32_t GetRgba() const;
				private:
					uint8_t m_nRed;
					uint8_t m_nGreen;
					uint8_t m_nBlue;
			};
			
			class FontImplementation;
			class Font
			{
				public:
					ND_DLL const char* GetName();
					ND_DLL void SetName(const char* szName);

					ND_DLL uint8_t GetSize();
					ND_DLL void SetSize(uint8_t nSize);

					ND_DLL const Color* GetColor();
					ND_DLL void SetColor(const Color& color);
					ND_DLL void ClearColor();
					
					ND_DLL bool GetBold();
					ND_DLL void SetBold(bool bBold);

					ND_DLL bool GetItalic();
					ND_DLL void SetItalic(bool bItalic);

					enum Underline
					{
						UNDERLINE_NONE = 0,
						UNDERLINE_SINGLE,
						UNDERLINE_DOUBLE,
						UNDERLINE_SINGLE_ACCOUNTING,
						UNDERLINE_DOUBLE_ACCOUNTING,
						NUM_UNDERLINE
					};
					ND_DLL Underline GetUnderline();
					ND_DLL void SetUnderline(Underline eUnderline);
					
				private:
					friend class WorkbookGlobals;
					friend class BiffWorkbookGlobals;
					friend class StyleImplementation;
					
					Font(WorkbookGlobals* pWorkbookGlobals);
					virtual ~Font();

					FontImplementation* m_pImplementation;
			};
			
			class LineImplementation;
			class Line
			{
				public:
					ND_DLL bool Equals(const Line* pLine) const;
					
					enum Type
					{
						TYPE_NONE = 0,
						TYPE_THIN,
						TYPE_DASHED,
						TYPE_DOTTED,
						TYPE_DASH_DOT,
						TYPE_DASH_DOT_DOT,
						TYPE_MEDIUM,
						TYPE_MEDIUM_DASHED,
						TYPE_MEDIUM_DASH_DOT,
						TYPE_MEDIUM_DASH_DOT_DOT,
						TYPE_THICK,

						// only for cell borders
						TYPE_DOUBLE,
						TYPE_HAIR,
						TYPE_SLANT_DASH_DOT_DOT,

						NUM_TYPE
					};
					
					ND_DLL Type GetType() const;
					ND_DLL void SetType(Type eType);
					
					ND_DLL const Color* GetColor() const;
					ND_DLL void SetColor(const Color& color);
						
				private:
					friend class StyleImplementation;
					friend class ChartImplementation;
					friend class SeriesImplementation;
					friend class SeriesStyleImplementation;
					friend class LegendImplementation;
					
					Line();
					~Line();

					LineImplementation* m_pImpl;
			};
		
			class FillImplementation;
			class Fill
			{
				public:
					ND_DLL bool Equals(const Fill* pFill) const;
					
					enum Type
					{
						TYPE_NONE = 0,
						TYPE_SOLID,
						NUM_TYPE
					};
					
					ND_DLL Type GetType() const;
					ND_DLL void SetType(Type eType);
					
					ND_DLL const Color* GetForegroundColor() const;
					ND_DLL void SetForegroundColor(const Color& foregroundColor);
					
					ND_DLL const Color* GetBackgroundColor() const;
					ND_DLL void SetBackgroundColor(const Color& backgroundColor);
				
				private:
					friend class ChartImplementation;
					friend class Series;
					friend class SeriesImplementation;
					friend class SeriesStyleImplementation;
					friend class LegendImplementation;
					
					Fill();
					~Fill();

					FillImplementation* m_pImpl;
			};
			
			class MarkerImplementation;
			class Marker
			{
				public:
					ND_DLL bool Equals(const Marker* pMarker) const;
					
					enum Type
					{
						TYPE_NONE = 0,
						TYPE_SQUARE,
						TYPE_DIAMOND,
						TYPE_TRIANGLE,
						TYPE_X,
						TYPE_ASTERISK,
						TYPE_SHORT_BAR,
						TYPE_LONG_BAR,
						TYPE_CIRCULAR,
						TYPE_PLUS,
						NUM_TYPE
					};
					
					ND_DLL Type GetType() const;
					ND_DLL void SetType(Type eType);
					
					ND_DLL const Color* GetFillColor() const;
					ND_DLL void SetFillColor(const Color& fillColor);
					ND_DLL void ClearFillColor();
					
					ND_DLL const Color* GetBorderColor() const;
					ND_DLL void SetBorderColor(const Color& borderColor);
					ND_DLL void ClearBorderColor();
					
					ND_DLL uint16_t GetSize() const;
					ND_DLL void SetSize(uint16_t nSize);
					
					
				private:
					friend class MarkerImplementation;
					friend class SeriesImplementation;
					friend class SeriesStyleImplementation;
					
					Marker();
					~Marker();

					MarkerImplementation* m_pImpl;
			};


		// ****************************************************************************
		// Merged Cells
		// ****************************************************************************
			class MergedCellImplementation;
			class MergedCell
			{
				public:
					ND_DLL uint16_t GetX() const;
					ND_DLL void SetX(uint16_t nX);

					ND_DLL uint16_t GetY() const;
					ND_DLL void SetY(uint16_t nY);

					ND_DLL uint16_t GetWidth() const;
					ND_DLL void SetWidth(uint16_t nWidth);

					ND_DLL uint16_t GetHeight() const;
					ND_DLL void SetHeight(uint16_t nHeight);

				private:
					friend class Worksheet;
					friend class BiffWorksheet;
					MergedCell(uint16_t nX, uint16_t nY, uint16_t nWidth, uint16_t nHeight);
					~MergedCell();

					MergedCellImplementation* m_pImplementation;
			};
	}
#endif