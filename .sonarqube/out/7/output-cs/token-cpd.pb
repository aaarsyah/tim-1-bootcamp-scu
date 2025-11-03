ó®
OD:\BootcampProject\default-project-main\src\08.BlazorUI\Services\UserService.cs
	namespace 	
MyApp
 
. 
BlazorUI 
. 
Services !
;! "
public 
class 
UserService 
: 
IUserService '
{ 
private 
readonly 
IHttpClientFactory '
_factory( 0
;0 1
public

 

UserService

 
(

 
IHttpClientFactory

 )
factory

* 1
)

1 2
{ 
_factory 
= 
factory 
; 
} 
public 

async 
Task 
< 
List 
< 
UserDto "
>" #
># $
GetAllUsersAsync% 5
(5 6%
AuthenticationHeaderValue6 O
authorizationP ]
)] ^
{ 
var 
_httpClient 
= 
_factory "
." #
CreateClient# /
(/ 0
$str0 8
)8 9
;9 :
_httpClient 
. !
DefaultRequestHeaders )
.) *
Authorization* 7
=8 9
authorization: G
;G H
try 
{ 	
var 
response 
= 
await  
_httpClient! ,
., -
GetAsync- 5
(5 6
$str6 P
)P Q
;Q R
if 
( 
! 
response 
. 
IsSuccessStatusCode -
)- .
{ 
return 
new 
( 
) 
; 
} 
var 
apiResponse 
= 
await #
response$ ,
., -
Content- 4
.4 5
ReadFromJsonAsync5 F
<F G
ApiResponseG R
<R S
IEnumerableS ^
<^ _
UserDto_ f
>f g
>g h
>h i
(i j
)j k
;k l
if 
( 
apiResponse 
? 
. 
Data !
==" $
null% )
)) *
{ 
return   
new   
(   
)   
;   
}!! 
return"" 
apiResponse"" 
."" 
Data"" #
.""# $
ToList""$ *
(""* +
)""+ ,
;"", -
}## 	
catch$$ 
($$ 
	Exception$$ 
ex$$ 
)$$ 
{%% 	
Console&& 
.&& 
	WriteLine&& 
(&& 
$"&&  
$str&&  9
{&&9 :
ex&&: <
.&&< =
Message&&= D
}&&D E
"&&E F
)&&F G
;&&G H
return'' 
new'' 
('' 
)'' 
;'' 
}(( 	
})) 
public** 

async** 
Task** 
<** 
List** 
<** 
RoleDto** "
>**" #
>**# $
GetAllRolesAsync**% 5
(**5 6%
AuthenticationHeaderValue**6 O
authorization**P ]
)**] ^
{++ 
var,, 
_httpClient,, 
=,, 
_factory,, "
.,," #
CreateClient,,# /
(,,/ 0
$str,,0 8
),,8 9
;,,9 :
_httpClient-- 
.-- !
DefaultRequestHeaders-- )
.--) *
Authorization--* 7
=--8 9
authorization--: G
;--G H
try.. 
{// 	
var00 
response00 
=00 
await00  
_httpClient00! ,
.00, -
GetAsync00- 5
(005 6
$str006 P
)00P Q
;00Q R
if11 
(11 
!11 
response11 
.11 
IsSuccessStatusCode11 -
)11- .
{22 
return33 
new33 
(33 
)33 
;33 
}44 
var66 
apiResponse66 
=66 
await66 #
response66$ ,
.66, -
Content66- 4
.664 5
ReadFromJsonAsync665 F
<66F G
ApiResponse66G R
<66R S
IEnumerable66S ^
<66^ _
RoleDto66_ f
>66f g
>66g h
>66h i
(66i j
)66j k
;66k l
if88 
(88 
apiResponse88 
?88 
.88 
Data88 !
==88" $
null88% )
)88) *
{99 
return:: 
new:: 
(:: 
):: 
;:: 
};; 
return<< 
apiResponse<< 
.<< 
Data<< #
.<<# $
ToList<<$ *
(<<* +
)<<+ ,
;<<, -
}== 	
catch>> 
(>> 
	Exception>> 
ex>> 
)>> 
{?? 	
Console@@ 
.@@ 
	WriteLine@@ 
(@@ 
$"@@  
$str@@  9
{@@9 :
ex@@: <
.@@< =
Message@@= D
}@@D E
"@@E F
)@@F G
;@@G H
returnAA 
newAA 
(AA 
)AA 
;AA 
}BB 	
}CC 
publicDD 

asyncDD 
TaskDD 
<DD 
boolDD 
>DD 
AddRoleToUserAsyncDD .
(DD. /%
AuthenticationHeaderValueDD/ H
authorizationDDI V
,DDV W
intDDX [
userIdDD\ b
,DDb c
RoleRequestDtoDDd r
requestDDs z
)DDz {
{EE 
varFF 
_httpClientFF 
=FF 
_factoryFF "
.FF" #
CreateClientFF# /
(FF/ 0
$strFF0 8
)FF8 9
;FF9 :
_httpClientGG 
.GG !
DefaultRequestHeadersGG )
.GG) *
AuthorizationGG* 7
=GG8 9
authorizationGG: G
;GGG H
tryHH 
{II 	
varJJ 
responseJJ 
=JJ 
awaitJJ  
_httpClientJJ! ,
.JJ, -
PutAsJsonAsyncJJ- ;
(JJ; <
$"JJ< >
$strJJ> W
{JJW X
userIdJJX ^
}JJ^ _
$strJJ_ i
"JJi j
,JJj k
requestJJl s
)JJs t
;JJt u
ifKK 
(KK 
!KK 
responseKK 
.KK 
IsSuccessStatusCodeKK -
)KK- .
{LL 
returnMM 
falseMM 
;MM 
}NN 
varOO 
apiResponseOO 
=OO 
awaitOO #
responseOO$ ,
.OO, -
ContentOO- 4
.OO4 5
ReadFromJsonAsyncOO5 F
<OOF G
ApiResponseOOG R
<OOR S
objectOOS Y
>OOY Z
>OOZ [
(OO[ \
)OO\ ]
;OO] ^
returnPP 
apiResponsePP 
!=PP !
nullPP" &
;PP& '
}QQ 	
catchRR 
(RR 
	ExceptionRR 
exRR 
)RR 
{SS 	
ConsoleTT 
.TT 
	WriteLineTT 
(TT 
$"TT  
$strTT  ;
{TT; <
exTT< >
.TT> ?
MessageTT? F
}TTF G
"TTG H
)TTH I
;TTI J
returnUU 
falseUU 
;UU 
}VV 	
}WW 
publicYY 

asyncYY 
TaskYY 
<YY 
boolYY 
>YY #
RemoveRoleFromUserAsyncYY 3
(YY3 4%
AuthenticationHeaderValueYY4 M
authorizationYYN [
,YY[ \
intYY] `
userIdYYa g
,YYg h
RoleRequestDtoYYi w
requestYYx 
)	YY Ä
{ZZ 
var[[ 
_httpClient[[ 
=[[ 
_factory[[ "
.[[" #
CreateClient[[# /
([[/ 0
$str[[0 8
)[[8 9
;[[9 :
_httpClient\\ 
.\\ !
DefaultRequestHeaders\\ )
.\\) *
Authorization\\* 7
=\\8 9
authorization\\: G
;\\G H
try]] 
{^^ 	
var__ 
response__ 
=__ 
await__  
_httpClient__! ,
.__, -
PutAsJsonAsync__- ;
(__; <
$"__< >
$str__> W
{__W X
userId__X ^
}__^ _
$str___ l
"__l m
,__m n
request__o v
)__v w
;__w x
if`` 
(`` 
!`` 
response`` 
.`` 
IsSuccessStatusCode`` -
)``- .
{aa 
returnbb 
falsebb 
;bb 
}cc 
vardd 
apiResponsedd 
=dd 
awaitdd #
responsedd$ ,
.dd, -
Contentdd- 4
.dd4 5
ReadFromJsonAsyncdd5 F
<ddF G
ApiResponseddG R
<ddR S
objectddS Y
>ddY Z
>ddZ [
(dd[ \
)dd\ ]
;dd] ^
returnee 
apiResponseee 
!=ee !
nullee" &
;ee& '
}ff 	
catchgg 
(gg 
	Exceptiongg 
exgg 
)gg 
{hh 	
Consoleii 
.ii 
	WriteLineii 
(ii 
$"ii  
$strii  @
{ii@ A
exiiA C
.iiC D
MessageiiD K
}iiK L
"iiL M
)iiM N
;iiN O
returnjj 
falsejj 
;jj 
}kk 	
}ll 
publicnn 

asyncnn 
Tasknn 
<nn 
boolnn 
>nn  
SetClaimForUserAsyncnn 0
(nn0 1%
AuthenticationHeaderValuenn1 J
authorizationnnK X
,nnX Y
intnnZ ]
userIdnn^ d
,nnd e
ClaimDtonnf n
requestnno v
)nnv w
{oo 
varpp 
_httpClientpp 
=pp 
_factorypp "
.pp" #
CreateClientpp# /
(pp/ 0
$strpp0 8
)pp8 9
;pp9 :
_httpClientqq 
.qq !
DefaultRequestHeadersqq )
.qq) *
Authorizationqq* 7
=qq8 9
authorizationqq: G
;qqG H
tryrr 
{ss 	
vartt 
responsett 
=tt 
awaittt  
_httpClienttt! ,
.tt, -
PutAsJsonAsynctt- ;
(tt; <
$"tt< >
$strtt> W
{ttW X
userIdttX ^
}tt^ _
$strtt_ j
"ttj k
,ttk l
requestttm t
)ttt u
;ttu v
ifuu 
(uu 
!uu 
responseuu 
.uu 
IsSuccessStatusCodeuu -
)uu- .
{vv 
returnww 
falseww 
;ww 
}xx 
varyy 
apiResponseyy 
=yy 
awaityy #
responseyy$ ,
.yy, -
Contentyy- 4
.yy4 5
ReadFromJsonAsyncyy5 F
<yyF G
ApiResponseyyG R
<yyR S
objectyyS Y
>yyY Z
>yyZ [
(yy[ \
)yy\ ]
;yy] ^
returnzz 
apiResponsezz 
!=zz !
nullzz" &
;zz& '
}{{ 	
catch|| 
(|| 
	Exception|| 
ex|| 
)|| 
{}} 	
Console~~ 
.~~ 
	WriteLine~~ 
(~~ 
$"~~  
$str~~  =
{~~= >
ex~~> @
.~~@ A
Message~~A H
}~~H I
"~~I J
)~~J K
;~~K L
return 
false 
; 
}
ÄÄ 	
}
ÅÅ 
public
ÉÉ 

async
ÉÉ 
Task
ÉÉ 
<
ÉÉ 
bool
ÉÉ 
>
ÉÉ &
RemoveClaimFromUserAsync
ÉÉ 4
(
ÉÉ4 5'
AuthenticationHeaderValue
ÉÉ5 N
authorization
ÉÉO \
,
ÉÉ\ ]
int
ÉÉ^ a
userId
ÉÉb h
,
ÉÉh i
ClaimDto
ÉÉj r
request
ÉÉs z
)
ÉÉz {
{
ÑÑ 
var
ÖÖ 
_httpClient
ÖÖ 
=
ÖÖ 
_factory
ÖÖ "
.
ÖÖ" #
CreateClient
ÖÖ# /
(
ÖÖ/ 0
$str
ÖÖ0 8
)
ÖÖ8 9
;
ÖÖ9 :
_httpClient
ÜÜ 
.
ÜÜ #
DefaultRequestHeaders
ÜÜ )
.
ÜÜ) *
Authorization
ÜÜ* 7
=
ÜÜ8 9
authorization
ÜÜ: G
;
ÜÜG H
try
áá 
{
àà 	
var
ââ 
response
ââ 
=
ââ 
await
ââ  
_httpClient
ââ! ,
.
ââ, -
PutAsJsonAsync
ââ- ;
(
ââ; <
$"
ââ< >
$str
ââ> W
{
ââW X
userId
ââX ^
}
ââ^ _
$str
ââ_ m
"
ââm n
,
âân o
request
ââp w
)
ââw x
;
ââx y
if
ää 
(
ää 
!
ää 
response
ää 
.
ää !
IsSuccessStatusCode
ää -
)
ää- .
{
ãã 
return
åå 
false
åå 
;
åå 
}
çç 
var
éé 
apiResponse
éé 
=
éé 
await
éé #
response
éé$ ,
.
éé, -
Content
éé- 4
.
éé4 5
ReadFromJsonAsync
éé5 F
<
ééF G
ApiResponse
ééG R
<
ééR S
object
ééS Y
>
ééY Z
>
ééZ [
(
éé[ \
)
éé\ ]
;
éé] ^
return
èè 
apiResponse
èè 
!=
èè !
null
èè" &
;
èè& '
}
êê 	
catch
ëë 
(
ëë 
	Exception
ëë 
ex
ëë 
)
ëë 
{
íí 	
Console
ìì 
.
ìì 
	WriteLine
ìì 
(
ìì 
$"
ìì  
$str
ìì  A
{
ììA B
ex
ììB D
.
ììD E
Message
ììE L
}
ììL M
"
ììM N
)
ììN O
;
ììO P
return
îî 
false
îî 
;
îî 
}
ïï 	
}
ññ 
public
òò 

async
òò 
Task
òò 
<
òò 
bool
òò 
>
òò 
ActivateUserAsync
òò -
(
òò- .'
AuthenticationHeaderValue
òò. G
authorization
òòH U
,
òòU V
int
òòW Z
userId
òò[ a
)
òòa b
{
ôô 
var
öö 
_httpClient
öö 
=
öö 
_factory
öö "
.
öö" #
CreateClient
öö# /
(
öö/ 0
$str
öö0 8
)
öö8 9
;
öö9 :
_httpClient
õõ 
.
õõ #
DefaultRequestHeaders
õõ )
.
õõ) *
Authorization
õõ* 7
=
õõ8 9
authorization
õõ: G
;
õõG H
try
úú 
{
ùù 	
var
ûû 
response
ûû 
=
ûû 
await
ûû  
_httpClient
ûû! ,
.
ûû, -
PutAsJsonAsync
ûû- ;
(
ûû; <
$"
ûû< >
$str
ûû> W
{
ûûW X
userId
ûûX ^
}
ûû^ _
$str
ûû_ h
"
ûûh i
,
ûûi j
new
ûûk n
{
ûûo p
}
ûûq r
)
ûûr s
;
ûûs t
if
üü 
(
üü 
!
üü 
response
üü 
.
üü !
IsSuccessStatusCode
üü -
)
üü- .
{
†† 
return
°° 
false
°° 
;
°° 
}
¢¢ 
var
££ 
apiResponse
££ 
=
££ 
await
££ #
response
££$ ,
.
££, -
Content
££- 4
.
££4 5
ReadFromJsonAsync
££5 F
<
££F G
ApiResponse
££G R
<
££R S
object
££S Y
>
££Y Z
>
££Z [
(
££[ \
)
££\ ]
;
££] ^
return
§§ 
apiResponse
§§ 
!=
§§ !
null
§§" &
;
§§& '
}
•• 	
catch
¶¶ 
(
¶¶ 
	Exception
¶¶ 
ex
¶¶ 
)
¶¶ 
{
ßß 	
Console
®® 
.
®® 
	WriteLine
®® 
(
®® 
$"
®®  
$str
®®  :
{
®®: ;
ex
®®; =
.
®®= >
Message
®®> E
}
®®E F
"
®®F G
)
®®G H
;
®®H I
return
©© 
false
©© 
;
©© 
}
™™ 	
}
´´ 
public
¨¨ 

async
¨¨ 
Task
¨¨ 
<
¨¨ 
bool
¨¨ 
>
¨¨ !
DeactivateUserAsync
¨¨ /
(
¨¨/ 0'
AuthenticationHeaderValue
¨¨0 I
authorization
¨¨J W
,
¨¨W X
int
¨¨Y \
userId
¨¨] c
)
¨¨c d
{
≠≠ 
var
ÆÆ 
_httpClient
ÆÆ 
=
ÆÆ 
_factory
ÆÆ "
.
ÆÆ" #
CreateClient
ÆÆ# /
(
ÆÆ/ 0
$str
ÆÆ0 8
)
ÆÆ8 9
;
ÆÆ9 :
_httpClient
ØØ 
.
ØØ #
DefaultRequestHeaders
ØØ )
.
ØØ) *
Authorization
ØØ* 7
=
ØØ8 9
authorization
ØØ: G
;
ØØG H
try
∞∞ 
{
±± 	
var
≤≤ 
response
≤≤ 
=
≤≤ 
await
≤≤  
_httpClient
≤≤! ,
.
≤≤, -
PutAsJsonAsync
≤≤- ;
(
≤≤; <
$"
≤≤< >
$str
≤≤> W
{
≤≤W X
userId
≤≤X ^
}
≤≤^ _
$str
≤≤_ j
"
≤≤j k
,
≤≤k l
new
≤≤m p
{
≤≤q r
}
≤≤s t
)
≤≤t u
;
≤≤u v
if
≥≥ 
(
≥≥ 
!
≥≥ 
response
≥≥ 
.
≥≥ !
IsSuccessStatusCode
≥≥ -
)
≥≥- .
{
¥¥ 
return
µµ 
false
µµ 
;
µµ 
}
∂∂ 
var
∑∑ 
apiResponse
∑∑ 
=
∑∑ 
await
∑∑ #
response
∑∑$ ,
.
∑∑, -
Content
∑∑- 4
.
∑∑4 5
ReadFromJsonAsync
∑∑5 F
<
∑∑F G
ApiResponse
∑∑G R
<
∑∑R S
object
∑∑S Y
>
∑∑Y Z
>
∑∑Z [
(
∑∑[ \
)
∑∑\ ]
;
∑∑] ^
return
∏∏ 
apiResponse
∏∏ 
!=
∏∏ !
null
∏∏" &
;
∏∏& '
}
ππ 	
catch
∫∫ 
(
∫∫ 
	Exception
∫∫ 
ex
∫∫ 
)
∫∫ 
{
ªª 	
Console
ºº 
.
ºº 
	WriteLine
ºº 
(
ºº 
$"
ºº  
$str
ºº  <
{
ºº< =
ex
ºº= ?
.
ºº? @
Message
ºº@ G
}
ººG H
"
ººH I
)
ººI J
;
ººJ K
return
ΩΩ 
false
ΩΩ 
;
ΩΩ 
}
ææ 	
}
øø 
public
¿¿ 

async
¿¿ 
Task
¿¿ 
<
¿¿ 
UserDto
¿¿ 
?
¿¿ 
>
¿¿ 
GetSelfUserAsync
¿¿  0
(
¿¿0 1'
AuthenticationHeaderValue
¿¿1 J
authorization
¿¿K X
)
¿¿X Y
{
¡¡ 
var
¬¬ 
_httpClient
¬¬ 
=
¬¬ 
_factory
¬¬ "
.
¬¬" #
CreateClient
¬¬# /
(
¬¬/ 0
$str
¬¬0 8
)
¬¬8 9
;
¬¬9 :
_httpClient
√√ 
.
√√ #
DefaultRequestHeaders
√√ )
.
√√) *
Authorization
√√* 7
=
√√8 9
authorization
√√: G
;
√√G H
try
ƒƒ 
{
≈≈ 	
var
∆∆ 
response
∆∆ 
=
∆∆ 
await
∆∆  
_httpClient
∆∆! ,
.
∆∆, -
GetAsync
∆∆- 5
(
∆∆5 6
$str
∆∆6 S
)
∆∆S T
;
∆∆T U
if
«« 
(
«« 
!
«« 
response
«« 
.
«« !
IsSuccessStatusCode
«« -
)
««- .
{
»» 
return
…… 
null
…… 
;
…… 
}
   
var
ÃÃ 
apiResponse
ÃÃ 
=
ÃÃ 
await
ÃÃ #
response
ÃÃ$ ,
.
ÃÃ, -
Content
ÃÃ- 4
.
ÃÃ4 5
ReadFromJsonAsync
ÃÃ5 F
<
ÃÃF G
ApiResponse
ÃÃG R
<
ÃÃR S
UserDto
ÃÃS Z
>
ÃÃZ [
>
ÃÃ[ \
(
ÃÃ\ ]
)
ÃÃ] ^
;
ÃÃ^ _
if
ŒŒ 
(
ŒŒ 
apiResponse
ŒŒ 
?
ŒŒ 
.
ŒŒ 
Data
ŒŒ !
==
ŒŒ" $
null
ŒŒ% )
)
ŒŒ) *
{
œœ 
return
–– 
null
–– 
;
–– 
}
—— 
return
““ 
apiResponse
““ 
.
““ 
Data
““ #
;
““# $
}
”” 	
catch
‘‘ 
(
‘‘ 
	Exception
‘‘ 
ex
‘‘ 
)
‘‘ 
{
’’ 	
Console
÷÷ 
.
÷÷ 
	WriteLine
÷÷ 
(
÷÷ 
$"
÷÷  
$str
÷÷  9
{
÷÷9 :
ex
÷÷: <
.
÷÷< =
Message
÷÷= D
}
÷÷D E
"
÷÷E F
)
÷÷F G
;
÷÷G H
return
◊◊ 
null
◊◊ 
;
◊◊ 
}
ÿÿ 	
}
ŸŸ 
}€€ Œ
RD:\BootcampProject\default-project-main\src\08.BlazorUI\Services\MyClassService.cs
	namespace 	
MyApp
 
. 
BlazorUI 
. 
Services !
;! "
public 
class 
MyClassService 
: 
IMyClassService -
{ 
private 
readonly 
IHttpClientFactory '
_factory( 0
;0 1
public

 

MyClassService

 
(

 
IHttpClientFactory 
factory "
)" #
{ 
_factory 
= 
factory 
; 
} 
public 

async 
Task 
< 
List 
< 

MyClassDto %
>% &
>& '
GetOwnMyClasses( 7
(7 8%
AuthenticationHeaderValue8 Q
authorizationR _
)_ `
{ 
var 
_httpClient 
= 
_factory "
." #
CreateClient# /
(/ 0
$str0 8
)8 9
;9 :
_httpClient 
. !
DefaultRequestHeaders )
.) *
Authorization* 7
=8 9
authorization: G
;G H
try 
{ 	
var 
response 
= 
await  
_httpClient! ,
., -
GetFromJsonAsync- =
<= >
List> B
<B C

MyClassDtoC M
>M N
>N O
(O P
$strP ]
)] ^
;^ _
return 
response 
? 
. 
OrderByDescending "
(" #
x# $
=>% '
x( )
.) *
Date* .
). /
. 
ToList 
( 
) 
?? 
new  
(  !
)! "
;" #
} 	
catch 
( 
	Exception 
ex 
) 
{ 	
Console 
. 
	WriteLine 
( 
$"  
$str  =
{= >
ex> @
.@ A
MessageA H
}H I
"I J
)J K
;K L
return 
new 
( 
) 
; 
} 	
}   
}!! ø
[D:\BootcampProject\default-project-main\src\08.BlazorUI\Services\Interfaces\IUserService.cs
	namespace 	
MyApp
 
. 
BlazorUI 
. 
Services !
;! "
public 
	interface 
IUserService 
{ 
Task 
< 	
List	 
< 
UserDto 
> 
> 
GetAllUsersAsync (
(( )%
AuthenticationHeaderValue) B
authorizationC P
)P Q
;Q R
Task		 
<		 	
List			 
<		 
RoleDto		 
>		 
>		 
GetAllRolesAsync		 (
(		( )%
AuthenticationHeaderValue		) B
authorization		C P
)		P Q
;		Q R
Task

 
<

 	
bool

	 
>

 
AddRoleToUserAsync

 !
(

! "%
AuthenticationHeaderValue

" ;
authorization

< I
,

I J
int

K N
userId

O U
,

U V
RoleRequestDto

W e
request

f m
)

m n
;

n o
Task 
< 	
bool	 
> #
RemoveRoleFromUserAsync &
(& '%
AuthenticationHeaderValue' @
authorizationA N
,N O
intP S
userIdT Z
,Z [
RoleRequestDto\ j
requestk r
)r s
;s t
Task 
< 	
bool	 
>  
SetClaimForUserAsync #
(# $%
AuthenticationHeaderValue$ =
authorization> K
,K L
intM P
userIdQ W
,W X
ClaimDtoY a
requestb i
)i j
;j k
Task 
< 	
bool	 
> $
RemoveClaimFromUserAsync '
(' (%
AuthenticationHeaderValue( A
authorizationB O
,O P
intQ T
userIdU [
,[ \
ClaimDto] e
requestf m
)m n
;n o
Task 
< 	
bool	 
> 
ActivateUserAsync  
(  !%
AuthenticationHeaderValue! :
authorization; H
,H I
intJ M
userIdN T
)T U
;U V
Task 
< 	
bool	 
> 
DeactivateUserAsync "
(" #%
AuthenticationHeaderValue# <
authorization= J
,J K
intL O
userIdP V
)V W
;W X
Task 
< 	
UserDto	 
? 
> 
GetSelfUserAsync #
(# $%
AuthenticationHeaderValue$ =
authorization> K
)K L
;L M
} ≈
^D:\BootcampProject\default-project-main\src\08.BlazorUI\Services\Interfaces\IMyClassService.cs
	namespace 	
MyApp
 
. 
BlazorUI 
. 
Services !
;! "
public 
	interface 
IMyClassService  
{ 
Task 
< 	
List	 
< 

MyClassDto 
> 
> 
GetOwnMyClasses *
(* +%
AuthenticationHeaderValue+ D
authorizationE R
)R S
;S T
}		 Ê
]D:\BootcampProject\default-project-main\src\08.BlazorUI\Services\Interfaces\ICourseService.cs
	namespace 	
MyApp
 
. 
BlazorUI 
. 
Services !
;! "
public 
	interface 
ICourseService 
{ 
Task 
< 	
IEnumerable	 
< 
	CourseDto 
> 
?  
>  !
GetAllCourseAsync" 3
(3 4
)4 5
;5 6
Task 
< 	
PaginatedResponse	 
< 
IEnumerable &
<& '
	CourseDto' 0
>0 1
>1 2
?2 3
>3 4&
GetAllCoursePaginatedAsync5 O
(O P
intP S
pageT X
,X Y
intZ ]
pageSize^ f
)f g
;g h
Task		 
<		 	
	CourseDto			 
?		 
>		 
GetCourseByIdAsync		 '
(		' (
int		( +
CourseId		, 4
)		4 5
;		5 6
Task

 
<

 	
List

	 
<

 
CategoryDto

 
>

 
>

 !
GetAllCategoriesAsync

 1
(

1 2
)

2 3
;

3 4
Task 
< 	
CategoryDto	 
? 
>  
GetCategoryByIdAsync +
(+ ,
int, /

CategoryId0 :
): ;
;; <
Task 
< 	
List	 
< 
	CourseDto 
> 
> $
GetCourseByCategoryAsync 2
(2 3
int3 6

CategoryId7 A
)A B
;B C
} ü
_D:\BootcampProject\default-project-main\src\08.BlazorUI\Services\Interfaces\ICheckoutService.cs
	namespace 	
MyApp
 
. 
BlazorUI 
. 
Services !
;! "
public 
	interface 
ICheckoutService !
{ 
Task 
< 	
List	 
< 
CartItemResponseDto !
>! "
>" #
GetSelfCartItem$ 3
(3 4%
AuthenticationHeaderValue4 M
authorizationN [
)[ \
;\ ]
Task		 
<		 	
bool			 
>		  
AddCourseToCartAsync		 #
(		# $%
AuthenticationHeaderValue		$ =
authorization		> K
,		K L
int		M P

scheduleId		Q [
)		[ \
;		\ ]
Task

 
<

 	
bool

	 
>

  
RemoveCourseFromCart

 #
(

# $%
AuthenticationHeaderValue

$ =
authorization

> K
,

K L
int

M P
cartId

Q W
)

W X
;

X Y
Task 
< 	
CheckoutResponseDto	 
? 
> 
CheckoutItemsAsync 1
(1 2%
AuthenticationHeaderValue2 K
authorizationL Y
,Y Z
CheckoutRequestDto[ m
requestn u
)u v
;v w
Task 
< 	
List	 
< 

PaymentDto 
> 
> 
GetAllPaymentsAsync .
(. /%
AuthenticationHeaderValue/ H
authorizationI V
)V W
;W X
Task 
< 	
List	 
< 

InvoiceDto 
> 
> #
GetAllInvoiceAdminAsync 2
(2 3%
AuthenticationHeaderValue3 L
authorizationM Z
)Z [
;[ \
Task 
< 	
List	 
< 

InvoiceDto 
> 
>  
GetSelfInvoicesAsync /
(/ 0%
AuthenticationHeaderValue0 I
authorizationJ W
)W X
;X Y
Task 
< 	

InvoiceDto	 
? 
> $
GetInvoiceByIdAdminAsync .
(. /%
AuthenticationHeaderValue/ H
authorizationI V
,V W
intX [
	invoiceId\ e
)e f
;f g
Task 
< 	

InvoiceDto	 
? 
> 
GetInvoiceByIdAsync )
() *%
AuthenticationHeaderValue* C
authorizationD Q
,Q R
intS V
	invoiceIdW `
)` a
;a b
Task 
< 	
List	 
< 
InvoiceDetailDto 
> 
>  2
&GetInvoiceDetailsByInvoiceIdAdminAsync! G
(G H%
AuthenticationHeaderValueH a
authorizationb o
,o p
intq t
	invoiceIdu ~
)~ 
;	 Ä
Task 
< 	
List	 
< 
InvoiceDetailDto 
> 
>  -
!GetInvoiceDetailsByInvoiceIdAsync! B
(B C%
AuthenticationHeaderValueC \
authorization] j
,j k
intl o
	invoiceIdp y
)y z
;z {
} ã
[D:\BootcampProject\default-project-main\src\08.BlazorUI\Services\Interfaces\IAuthService.cs
	namespace 	
MyApp
 
. 
BlazorUI 
. 
Services !
;! "
public 
	interface 
IAuthService 
{ 
Task 
< 	
AuthResponseDto	 
? 
> 

LoginAsync %
(% &
LoginRequestDto& 5
request6 =
)= >
;> ?
Task		 
<		 	
bool			 
>		 
RegisterAsync		 
(		 
RegisterRequestDto		 /
request		0 7
)		7 8
;		8 9
Task

 
<

 	
bool

	 
>

 
ConfirmEmailAsync

  
(

  !"
ConfirmEmailRequestDto

! 7
request

8 ?
)

? @
;

@ A
Task 
< 	
bool	 
> 
ChangePasswordAsync "
(" #%
AuthenticationHeaderValue# <
authorization= J
,J K$
ChangePasswordRequestDtoL d
requeste l
)l m
;m n
Task 
< 	
bool	 
> 
ForgotPasswordAsync "
(" #$
ForgotPasswordRequestDto# ;
request< C
)C D
;D E
Task 
< 	
bool	 
> 
ResetPasswordAsync !
(! "#
ResetPasswordRequestDto" 9
request: A
)A B
;B C
Task 
< 	
UserDto	 
? 
> 
GetCurrentUserAsync &
(& '%
AuthenticationHeaderValue' @
authorizationA N
)N O
;O P
Task 
LogoutAsync	 
( %
AuthenticationHeaderValue .
authorization/ <
)< =
;= >
Task 
< 	
bool	 
> 
IsLoggedInAsync 
( 
)  
;  !
Task 
< 	
bool	 
> 
IsAdminAsync 
( 
) 
; 
Task 
< 	%
AuthenticationHeaderValue	 "
?" #
># $
GetAccessTokenAsync% 8
(8 9
)9 :
;: ;
} ‹
\D:\BootcampProject\default-project-main\src\08.BlazorUI\Services\Interfaces\IAdminService.cs
	namespace 	
MyApp
 
. 
BlazorUI 
. 
Services !
;! "
public 
	interface 
IAdminService 
{ 
Task 
< 	
List	 
< 
	CourseDto 
> 
> 
GetAllCourseAsync +
(+ ,
), -
;- .
Task		 
<		 	
	CourseDto			 
?		 
>		 
CreateCourseAsync		 &
(		& '%
AuthenticationHeaderValue		' @
authorization		A N
,		N O
CreateCourseDto		P _
request		` g
)		g h
;		h i
Task

 
<

 	
	CourseDto

	 
?

 
>

 
UpdateCourseAsync

 &
(

& '%
AuthenticationHeaderValue

' @
authorization

A N
,

N O
int

P S
id

T V
,

V W
UpdateCourseDto

X g
request

h o
)

o p
;

p q
Task 
< 	
bool	 
> 
DeleteCourseAsync  
(  !%
AuthenticationHeaderValue! :
authorization; H
,H I
intJ M
idN P
)P Q
;Q R
Task 
< 	
List	 
< 
CategoryDto 
> 
> 
GetAllCategoryAsync /
(/ 0
)0 1
;1 2
Task 
< 	
CategoryDto	 
? 
> 
CreateCategoryAsync *
(* +%
AuthenticationHeaderValue+ D
authorizationE R
,R S
CreateCategoryDtoT e
requestf m
)m n
;n o
Task 
< 	
CategoryDto	 
? 
> 
UpdateCategoryAsync *
(* +%
AuthenticationHeaderValue+ D
authorizationE R
,R S
intT W
idX Z
,Z [
UpdateCategoryDto\ m
requestn u
)u v
;v w
Task 
< 	
bool	 
> 
DeleteCategoryAsync "
(" #%
AuthenticationHeaderValue# <
authorization= J
,J K
intL O
idP R
)R S
;S T
Task 
< 	

PaymentDto	 
? 
> $
CreatePaymentMethodAsync .
(. /%
AuthenticationHeaderValue/ H
authorizationI V
,V W
CreatePaymentDtoX h
paymenti p
)p q
;q r
Task 
< 	

PaymentDto	 
? 
> $
UpdatePaymentMethodAsync .
(. /%
AuthenticationHeaderValue/ H
authorizationI V
,V W
intX [
id\ ^
,^ _
UpdatePaymentDto` p
paymentq x
)x y
;y z
Task 
< 	
bool	 
> $
DeletePaymentMethodAsync '
(' (%
AuthenticationHeaderValue( A
authorizationB O
,O P
intQ T
idU W
)W X
;X Y
} ±L
PD:\BootcampProject\default-project-main\src\08.BlazorUI\Services\ErrorService.cs
	namespace 	
MyApp
 
. 
BlazorUI 
. 
Services !
;! "
public 
class 
ErrorService 
{ 
public 

ErrorService 
( 
) 
{ 
} 
public 

async 
Task 
< 
string 
? 
> 
GetErrorAsync ,
(, -
HttpResponseMessage- @
responseA I
)I J
{ 
if		 

(		 
response		 
.		 
IsSuccessStatusCode		 (
)		( )
{

 	
return 
null 
; 
} 	
switch 
( 
response 
. 

StatusCode #
)# $
{ 	
case 
System 
. 
Net 
. 
HttpStatusCode *
.* +

BadRequest+ 5
:5 6
try 
{ 
var 
problemDetails &
=' (
await) .
response/ 7
.7 8
Content8 ?
.? @
ReadFromJsonAsync@ Q
<Q R
ProblemDetailsR `
>` a
(a b
)b c
;c d
if 
( 
problemDetails &
==' )
null* .
). /
throw0 5
new6 9
	Exception: C
(C D
)D E
;E F
var 
error 
= 
problemDetails  .
.. /

Extensions/ 9
[9 :
$str: E
]E F
?F G
.G H
ToStringH P
(P Q
)Q R
;R S
if 
( 
error 
==  
$str! 3
)3 4
{ 
return 
problemDetails -
.- .
Title. 3
;3 4
} 
if 
( 
problemDetails &
.& '
Title' ,
==- /
null0 4
)4 5
throw6 ;
new< ?
	Exception@ I
(I J
)J K
;K L
if 
( 
problemDetails &
.& '
Title' ,
==- /
$str0 Y
)Y Z
{ 
return 
string %
.% &
Join& *
(* +
$str+ .
,. /
problemDetails0 >
.> ?

Extensions? I
[I J
$strJ R
]R S
)S T
;T U
} 
return 
problemDetails )
.) *
Title* /
;/ 0
} 
catch   
(   
	Exception    
)    !
{!! 
return"" 
$str"" 6
;""6 7
}## 
case$$ 
System$$ 
.$$ 
Net$$ 
.$$ 
HttpStatusCode$$ *
.$$* +
Unauthorized$$+ 7
:$$7 8
try%% 
{&& 
var'' 
problemDetails'' &
=''' (
await'') .
response''/ 7
.''7 8
Content''8 ?
.''? @
ReadFromJsonAsync''@ Q
<''Q R
ProblemDetails''R `
>''` a
(''a b
)''b c
;''c d
if(( 
((( 
problemDetails(( &
==((' )
null((* .
)((. /
throw((0 5
new((6 9
	Exception((: C
(((C D
)((D E
;((E F
var)) 
error)) 
=)) 
problemDetails))  .
.)). /

Extensions))/ 9
[))9 :
$str)): E
]))E F
?))F G
.))G H
ToString))H P
())P Q
)))Q R
;))R S
if** 
(** 
error** 
==**  
$str**! 0
)**0 1
{++ 
return,, 
problemDetails,, -
.,,- .
Title,,. 3
;,,3 4
}-- 
if.. 
(.. 
problemDetails.. &
...& '
Title..' ,
==..- /
null..0 4
)..4 5
throw..6 ;
new..< ?
	Exception..@ I
(..I J
)..J K
;..K L
return00 
problemDetails00 )
.00) *
Title00* /
;00/ 0
}11 
catch22 
(22 
	Exception22  
)22  !
{33 
return44 
$str44 2
;442 3
}55 
case66 
System66 
.66 
Net66 
.66 
HttpStatusCode66 *
.66* +
	Forbidden66+ 4
:664 5
try77 
{88 
var99 
problemDetails99 &
=99' (
await99) .
response99/ 7
.997 8
Content998 ?
.99? @
ReadFromJsonAsync99@ Q
<99Q R
ProblemDetails99R `
>99` a
(99a b
)99b c
;99c d
if:: 
(:: 
problemDetails:: &
==::' )
null::* .
)::. /
throw::0 5
new::6 9
	Exception::: C
(::C D
)::D E
;::E F
var;; 
error;; 
=;; 
problemDetails;;  .
.;;. /

Extensions;;/ 9
[;;9 :
$str;;: E
];;E F
?;;F G
.;;G H
ToString;;H P
(;;P Q
);;Q R
;;;R S
if<< 
(<< 
error<< 
==<<  
$str<<! 1
)<<1 2
{== 
return>> 
problemDetails>> -
.>>- .
Title>>. 3
;>>3 4
}?? 
if@@ 
(@@ 
error@@ 
==@@  
$str@@! 3
)@@3 4
{AA 
returnBB 
problemDetailsBB -
.BB- .
TitleBB. 3
;BB3 4
}CC 
ifDD 
(DD 
problemDetailsDD &
.DD& '
TitleDD' ,
==DD- /
nullDD0 4
)DD4 5
throwDD6 ;
newDD< ?
	ExceptionDD@ I
(DDI J
)DDJ K
;DDK L
returnEE 
problemDetailsEE )
.EE) *
TitleEE* /
;EE/ 0
}FF 
catchGG 
(GG 
	ExceptionGG  
)GG  !
{HH 
returnII 
$strII D
;IID E
}JJ 
caseKK 
SystemKK 
.KK 
NetKK 
.KK 
HttpStatusCodeKK *
.KK* +
NotFoundKK+ 3
:KK3 4
tryLL 
{MM 
varNN 
problemDetailsNN &
=NN' (
awaitNN) .
responseNN/ 7
.NN7 8
ContentNN8 ?
.NN? @
ReadFromJsonAsyncNN@ Q
<NNQ R
ProblemDetailsNNR `
>NN` a
(NNa b
)NNb c
;NNc d
ifOO 
(OO 
problemDetailsOO &
==OO' )
nullOO* .
)OO. /
throwOO0 5
newOO6 9
	ExceptionOO: C
(OOC D
)OOD E
;OOE F
varPP 
errorPP 
=PP 
problemDetailsPP  .
.PP. /

ExtensionsPP/ 9
[PP9 :
$strPP: E
]PPE F
?PPF G
.PPG H
ToStringPPH P
(PPP Q
)PPQ R
;PPR S
ifQQ 
(QQ 
errorQQ 
==QQ  
$strQQ! ,
)QQ, -
{RR 
returnSS 
problemDetailsSS -
.SS- .
TitleSS. 3
;SS3 4
}TT 
ifUU 
(UU 
problemDetailsUU &
.UU& '
TitleUU' ,
==UU- /
nullUU0 4
)UU4 5
throwUU6 ;
newUU< ?
	ExceptionUU@ I
(UUI J
)UUJ K
;UUK L
returnVV 
problemDetailsVV )
.VV) *
TitleVV* /
;VV/ 0
}WW 
catchXX 
(XX 
	ExceptionXX  
)XX  !
{YY 
returnZZ 
$strZZ 9
;ZZ9 :
}[[ 
case\\ 
System\\ 
.\\ 
Net\\ 
.\\ 
HttpStatusCode\\ *
.\\* +
InternalServerError\\+ >
:\\> ?
try]] 
{^^ 
var__ 
problemDetails__ &
=__' (
await__) .
response__/ 7
.__7 8
Content__8 ?
.__? @
ReadFromJsonAsync__@ Q
<__Q R
ProblemDetails__R `
>__` a
(__a b
)__b c
;__c d
if`` 
(`` 
problemDetails`` &
==``' )
null``* .
)``. /
throw``0 5
new``6 9
	Exception``: C
(``C D
)``D E
;``E F
ifaa 
(aa 
problemDetailsaa &
.aa& '
Titleaa' ,
==aa- /
nullaa0 4
)aa4 5
throwaa6 ;
newaa< ?
	Exceptionaa@ I
(aaI J
)aaJ K
;aaK L
returnbb 
problemDetailsbb )
.bb) *
Titlebb* /
;bb/ 0
}cc 
catchdd 
(dd 
	Exceptiondd  
)dd  !
{ee 
returnff 
$strff 2
;ff2 3
}gg 
defaulthh 
:hh 
returnii 
$strii 
;ii 
}jj 	
}kk 
}ll Í5
[D:\BootcampProject\default-project-main\src\08.BlazorUI\Services\CustomAuthStateProvider.cs
	namespace 	
MyApp
 
. 
BlazorUI 
. 
Services !
;! "
public 
class #
CustomAuthStateProvider $
:% &'
AuthenticationStateProvider' B
{		 
private

 
readonly

  
ILocalStorageService

 )
_localStorage

* 7
;

7 8
private 
readonly 
IHttpClientFactory '
_factory( 0
;0 1
private 
readonly 
AuthenticationState (

_anonymous) 3
;3 4
public 
#
CustomAuthStateProvider "
(" # 
ILocalStorageService# 7
localStorage8 D
,D E
IHttpClientFactoryF X
factoryY `
)` a
{ 
_localStorage 
= 
localStorage $
;$ %
_factory 
= 
factory 
; 

_anonymous 
= 
new 
AuthenticationState ,
(, -
new- 0
ClaimsPrincipal1 @
(@ A
newA D
ClaimsIdentityE S
(S T
)T U
)U V
)V W
;W X
} 
public 

override 
async 
Task 
< 
AuthenticationState 2
>2 3'
GetAuthenticationStateAsync4 O
(O P
)P Q
{ 
var 
_httpClient 
= 
_factory "
." #
CreateClient# /
(/ 0
$str0 8
)8 9
;9 :
try 
{ 	
var 
token 
= 
await 
_localStorage +
.+ ,
GetItemAsync, 8
<8 9
string9 ?
>? @
(@ A
$strA L
)L M
;M N
if 
( 
string 
. 
IsNullOrWhiteSpace )
() *
token* /
)/ 0
)0 1
return 

_anonymous !
;! "
_httpClient 
. !
DefaultRequestHeaders -
.- .
Authorization. ;
=< =
new> A%
AuthenticationHeaderValueB [
([ \
$str\ d
,d e
tokenf k
)k l
;l m
var   
claims   
=   
ParseClaimsFromJwt   +
(  + ,
token  , 1
)  1 2
;  2 3
var!! 
expiry!! 
=!! 
claims!! 
.!!  
FirstOrDefault!!  .
(!!. /
c!!/ 0
=>!!1 3
c!!4 5
.!!5 6
Type!!6 :
==!!; =
$str!!> C
)!!C D
?!!D E
.!!E F
Value!!F K
;!!K L
if## 
(## 
expiry## 
!=## 
null## 
)## 
{$$ 
var%% 
expiryDateTime%% "
=%%# $
DateTimeOffset%%% 3
.%%3 4
FromUnixTimeSeconds%%4 G
(%%G H
long%%H L
.%%L M
Parse%%M R
(%%R S
expiry%%S Y
)%%Y Z
)%%Z [
;%%[ \
if'' 
('' 
expiryDateTime'' "
<=''# %
DateTimeOffset''& 4
.''4 5
UtcNow''5 ;
)''; <
{(( 
await)) 
_localStorage)) '
.))' (
RemoveItemAsync))( 7
())7 8
$str))8 C
)))C D
;))D E
return** 

_anonymous** %
;**% &
}++ 
},, 
var.. 
user.. 
=.. 
new.. 
ClaimsPrincipal.. *
(..* +
new..+ .
ClaimsIdentity../ =
(..= >
claims..> D
,..D E
$str..F K
)..K L
)..L M
;..M N
return// 
new// 
AuthenticationState// *
(//* +
user//+ /
)/// 0
;//0 1
}00 	
catch11 
(11 
	Exception11 
)11 
{22 	
return33 

_anonymous33 
;33 
}44 	
}55 
public77 

void77 $
NotifyUserAuthentication77 (
(77( )
string77) /
token770 5
)775 6
{88 
var99 
claims99 
=99 
ParseClaimsFromJwt99 '
(99' (
token99( -
)99- .
;99. /
var:: 
authenticatedUser:: 
=:: 
new::  #
ClaimsPrincipal::$ 3
(::3 4
new::4 7
ClaimsIdentity::8 F
(::F G
claims::G M
,::M N
$str::O T
)::T U
)::U V
;::V W
var;; 
	authState;; 
=;; 
Task;; 
.;; 

FromResult;; '
(;;' (
new;;( +
AuthenticationState;;, ?
(;;? @
authenticatedUser;;@ Q
);;Q R
);;R S
;;;S T,
 NotifyAuthenticationStateChanged<< (
(<<( )
	authState<<) 2
)<<2 3
;<<3 4
}== 
public?? 

void?? 
NotifyUserLogout??  
(??  !
)??! "
{@@ 
varAA 
	authStateAA 
=AA 
TaskAA 
.AA 

FromResultAA '
(AA' (

_anonymousAA( 2
)AA2 3
;AA3 4,
 NotifyAuthenticationStateChangedBB (
(BB( )
	authStateBB) 2
)BB2 3
;BB3 4
}CC 
privateEE 
IEnumerableEE 
<EE 
ClaimEE 
>EE 
ParseClaimsFromJwtEE 1
(EE1 2
stringEE2 8
jwtEE9 <
)EE< =
{FF 
varGG 
handlerGG 
=GG 
newGG #
JwtSecurityTokenHandlerGG 1
(GG1 2
)GG2 3
;GG3 4
varHH 
tokenHH 
=HH 
handlerHH 
.HH 
ReadJwtTokenHH (
(HH( )
jwtHH) ,
)HH, -
;HH- .
returnII 
tokenII 
.II 
ClaimsII 
;II 
}JJ 
publicKK 

asyncKK 
TaskKK 
<KK 
boolKK 
>KK 
isLoggedInAsyncKK +
(KK+ ,
)KK, -
{LL 
returnMM 
awaitMM '
GetAuthenticationStateAsyncMM 0
(MM0 1
)MM1 2
!=MM3 5

_anonymousMM6 @
;MM@ A
}NN 
}OO ‹ñ
QD:\BootcampProject\default-project-main\src\08.BlazorUI\Services\CourseService.cs
	namespace 	
MyApp
 
. 
BlazorUI 
. 
Services !
;! "
public 
class 
CourseService 
: 
ICourseService +
{ 
private 
readonly 
IHttpClientFactory '
_factory( 0
;0 1
public

 

CourseService

 
(

 
IHttpClientFactory

 +
factory

, 3
)

3 4
{ 
_factory 
= 
factory 
; 
} 
public 

async 
Task 
< 
IEnumerable !
<! "
	CourseDto" +
>+ ,
?, -
>- .
GetAllCourseAsync/ @
(@ A
)A B
{ 
var 
_httpClient 
= 
_factory "
." #
CreateClient# /
(/ 0
$str0 8
)8 9
;9 :
var 
	parameter 
= 
new !
CourseQueryParameters 1
(1 2
)2 3
;3 4
var 
query 
= 
new 

Dictionary "
<" #
string# )
,) *
string+ 1
?1 2
>2 3
{ 	
[ 
$str 
] 
= 
	parameter "
." #
Search# )
,) *
[ 
$str 
] 
= 
	parameter &
.& '

CategoryId' 1
.1 2
ToString2 :
(: ;
); <
,< =
[ 
$str 
] 
= 
	parameter $
.$ %
MinPrice% -
.- .
ToString. 6
(6 7
)7 8
,8 9
[ 
$str 
] 
= 
	parameter $
.$ %
MaxPrice% -
.- .
ToString. 6
(6 7
)7 8
,8 9
[ 
$str 
] 
= 
	parameter "
." #
SortBy# )
,) *
[ 
$str 
] 
= 
	parameter  )
.) *
SortDirection* 7
,7 8
} 	
;	 

try   
{!! 	
var"" 
response"" 
="" 
await""  
_httpClient""! ,
."", -
GetAsync""- 5
(""5 6
QueryHelpers""6 B
.""B C
AddQueryString""C Q
(""Q R
$str""R a
,""a b
query""c h
)""h i
)""i j
;""j k
if## 
(## 
response## 
.## 
IsSuccessStatusCode## ,
)##, -
{$$ 
var%% 
apiResponse%% 
=%%  !
await%%" '
response%%( 0
.%%0 1
Content%%1 8
.%%8 9
ReadFromJsonAsync%%9 J
<%%J K
ApiResponse%%K V
<%%V W
PaginatedResponse%%W h
<%%h i
IEnumerable%%i t
<%%t u
	CourseDto%%u ~
>%%~ 
>	%% Ä
>
%%Ä Å
>
%%Å Ç
(
%%Ç É
)
%%É Ñ
;
%%Ñ Ö
if'' 
('' 
apiResponse'' 
?''  
.''  !
Data''! %
?''% &
.''& '
Data''' +
!='', .
null''/ 3
)''3 4
{(( 
return)) 
apiResponse)) &
.))& '
Data))' +
.))+ ,
Data)), 0
.** 
OrderBy**  
(**  !
c**! "
=>**# %
c**& '
.**' (
Id**( *
)*** +
.++ 
ToList++ 
(++  
)++  !
;++! "
},, 
return-- 
null-- 
;-- 
}.. 
return// 
null// 
;// 
}00 	
catch11 
(11 
	Exception11 
ex11 
)11 
{22 	
Console33 
.33 
	WriteLine33 
(33 
$"33  
$str33  9
{339 :
ex33: <
.33< =
Message33= D
}33D E
"33E F
)33F G
;33G H
return44 
null44 
;44 
}55 	
}66 
public77 

async77 
Task77 
<77 
PaginatedResponse77 '
<77' (
IEnumerable77( 3
<773 4
	CourseDto774 =
>77= >
>77> ?
?77? @
>77@ A&
GetAllCoursePaginatedAsync77B \
(77\ ]
int77] `
page77a e
,77e f
int77g j
pageSize77k s
)77s t
{88 
var:: 
_httpClient:: 
=:: 
_factory:: "
.::" #
CreateClient::# /
(::/ 0
$str::0 8
)::8 9
;::9 :
var<< 
	parameter<< 
=<< 
new<< !
CourseQueryParameters<< 1
(<<1 2
)<<2 3
;<<3 4
var== 
query== 
=== 
new== 

Dictionary== "
<==" #
string==# )
,==) *
string==+ 1
?==1 2
>==2 3
{>> 	
[?? 
$str?? 
]?? 
=?? 
page?? !
.??! "
ToString??" *
(??* +
)??+ ,
,??, -
[@@ 
$str@@ 
]@@ 
=@@ 
pageSize@@ #
.@@# $
ToString@@$ ,
(@@, -
)@@- .
,@@. /
[AA 
$strAA 
]AA 
=AA 
	parameterAA "
.AA" #
SearchAA# )
,AA) *
[BB 
$strBB 
]BB 
=BB 
	parameterBB &
.BB& '

CategoryIdBB' 1
.BB1 2
ToStringBB2 :
(BB: ;
)BB; <
,BB< =
[CC 
$strCC 
]CC 
=CC 
	parameterCC $
.CC$ %
MinPriceCC% -
.CC- .
ToStringCC. 6
(CC6 7
)CC7 8
,CC8 9
[DD 
$strDD 
]DD 
=DD 
	parameterDD $
.DD$ %
MaxPriceDD% -
.DD- .
ToStringDD. 6
(DD6 7
)DD7 8
,DD8 9
[EE 
$strEE 
]EE 
=EE 
	parameterEE "
.EE" #
SortByEE# )
,EE) *
[FF 
$strFF 
]FF 
=FF 
	parameterFF  )
.FF) *
SortDirectionFF* 7
,FF7 8
}HH 	
;HH	 

tryII 
{JJ 	
varKK 
responseKK 
=KK 
awaitKK  
_httpClientKK! ,
.KK, -
GetAsyncKK- 5
(KK5 6
QueryHelpersKK6 B
.KKB C
AddQueryStringKKC Q
(KKQ R
$strKKR a
,KKa b
queryKKc h
)KKh i
)KKi j
;KKj k
ifLL 
(LL 
responseLL 
.LL 
IsSuccessStatusCodeLL ,
)LL, -
{MM 
varNN 
apiResponseNN 
=NN  !
awaitNN" '
responseNN( 0
.NN0 1
ContentNN1 8
.NN8 9
ReadFromJsonAsyncNN9 J
<NNJ K
ApiResponseNNK V
<NNV W
PaginatedResponseNNW h
<NNh i
IEnumerableNNi t
<NNt u
	CourseDtoNNu ~
>NN~ 
>	NN Ä
>
NNÄ Å
>
NNÅ Ç
(
NNÇ É
)
NNÉ Ñ
;
NNÑ Ö
ifPP 
(PP 
apiResponsePP 
?PP  
.PP  !
DataPP! %
!=PP& (
nullPP) -
)PP- .
{QQ 
returnRR 
apiResponseRR &
.RR& '
DataRR' +
;RR+ ,
}SS 
returnTT 
nullTT 
;TT 
}UU 
returnVV 
nullVV 
;VV 
}WW 	
catchXX 
(XX 
	ExceptionXX 
exXX 
)XX 
{YY 	
ConsoleZZ 
.ZZ 
	WriteLineZZ 
(ZZ 
$"ZZ  
$strZZ  9
{ZZ9 :
exZZ: <
.ZZ< =
MessageZZ= D
}ZZD E
"ZZE F
)ZZF G
;ZZG H
return[[ 
null[[ 
;[[ 
}\\ 	
}]] 
public^^ 

async^^ 
Task^^ 
<^^ 
	CourseDto^^ 
?^^  
>^^  !
GetCourseByIdAsync^^" 4
(^^4 5
int^^5 8
CourseId^^9 A
)^^A B
{__ 
varaa 
_httpClientaa 
=aa 
_factoryaa "
.aa" #
CreateClientaa# /
(aa/ 0
$straa0 8
)aa8 9
;aa9 :
trybb 
{cc 	
vardd 
responsedd 
=dd 
awaitdd  
_httpClientdd! ,
.dd, -
GetAsyncdd- 5
(dd5 6
$"dd6 8
$strdd8 C
{ddC D
CourseIdddD L
}ddL M
"ddM N
)ddN O
;ddO P
ifee 
(ee 
responseee 
.ee 
IsSuccessStatusCodeee ,
)ee, -
{ff 
vargg 
apiResponsegg 
=gg  !
awaitgg" '
responsegg( 0
.gg0 1
Contentgg1 8
.gg8 9
ReadFromJsonAsyncgg9 J
<ggJ K
ApiResponseggK V
<ggV W
	CourseDtoggW `
>gg` a
>gga b
(ggb c
)ggc d
;ggd e
ifii 
(ii 
apiResponseii 
?ii  
.ii  !
Dataii! %
!=ii& (
nullii) -
)ii- .
{jj 
returnkk 
apiResponsekk &
.kk& '
Datakk' +
;kk+ ,
}ll 
returnmm 
nullmm 
;mm 
}nn 
returnoo 
nulloo 
;oo 
}pp 	
catchqq 
(qq 
	Exceptionqq 
exqq 
)qq 
{rr 	
Consoless 
.ss 
	WriteLiness 
(ss 
$"ss  
$strss  2
{ss2 3
exss3 5
.ss5 6
Messagess6 =
}ss= >
"ss> ?
)ss? @
;ss@ A
returntt 
nulltt 
;tt 
}uu 	
}vv 
publicww 

asyncww 
Taskww 
<ww 
Listww 
<ww 
CategoryDtoww &
>ww& '
>ww' (!
GetAllCategoriesAsyncww) >
(ww> ?
)ww? @
{xx 
varzz 
_httpClientzz 
=zz 
_factoryzz "
.zz" #
CreateClientzz# /
(zz/ 0
$strzz0 8
)zz8 9
;zz9 :
try{{ 
{|| 	
var}} 
response}} 
=}} 
await}}  
_httpClient}}! ,
.}}, -
GetAsync}}- 5
(}}5 6
$str}}6 D
)}}D E
;}}E F
if~~ 
(~~ 
response~~ 
.~~ 
IsSuccessStatusCode~~ ,
)~~, -
{ 
var
ÄÄ 
apiResponse
ÄÄ 
=
ÄÄ  !
await
ÄÄ" '
response
ÄÄ( 0
.
ÄÄ0 1
Content
ÄÄ1 8
.
ÄÄ8 9
ReadFromJsonAsync
ÄÄ9 J
<
ÄÄJ K
ApiResponse
ÄÄK V
<
ÄÄV W
List
ÄÄW [
<
ÄÄ[ \
CategoryDto
ÄÄ\ g
>
ÄÄg h
>
ÄÄh i
>
ÄÄi j
(
ÄÄj k
)
ÄÄk l
;
ÄÄl m
if
ÇÇ 
(
ÇÇ 
apiResponse
ÇÇ 
?
ÇÇ  
.
ÇÇ  !
Data
ÇÇ! %
!=
ÇÇ& (
null
ÇÇ) -
)
ÇÇ- .
{
ÉÉ 
return
ÑÑ 
apiResponse
ÑÑ &
.
ÑÑ& '
Data
ÑÑ' +
;
ÑÑ+ ,
}
ÖÖ 
return
ÜÜ 
new
ÜÜ 
(
ÜÜ 
)
ÜÜ 
;
ÜÜ 
}
áá 
return
àà 
new
àà 
(
àà 
)
àà 
;
àà 
}
ââ 	
catch
ää 
(
ää 
	Exception
ää 
ex
ää 
)
ää 
{
ãã 	
Console
åå 
.
åå 
	WriteLine
åå 
(
åå 
$"
åå  
$str
åå  9
{
åå9 :
ex
åå: <
.
åå< =
Message
åå= D
}
ååD E
"
ååE F
)
ååF G
;
ååG H
return
çç 
new
çç 
(
çç 
)
çç 
;
çç 
}
éé 	
}
èè 
public
êê 

async
êê 
Task
êê 
<
êê 
CategoryDto
êê !
?
êê! "
>
êê" #"
GetCategoryByIdAsync
êê$ 8
(
êê8 9
int
êê9 <

CategoryId
êê= G
)
êêG H
{
ëë 
var
ìì 
_httpClient
ìì 
=
ìì 
_factory
ìì "
.
ìì" #
CreateClient
ìì# /
(
ìì/ 0
$str
ìì0 8
)
ìì8 9
;
ìì9 :
try
îî 
{
ïï 	
var
ññ 
response
ññ 
=
ññ 
await
ññ  
_httpClient
ññ! ,
.
ññ, -
GetAsync
ññ- 5
(
ññ5 6
$"
ññ6 8
$str
ññ8 E
{
ññE F

CategoryId
ññF P
}
ññP Q
"
ññQ R
)
ññR S
;
ññS T
if
óó 
(
óó 
response
óó 
.
óó !
IsSuccessStatusCode
óó ,
)
óó, -
{
òò 
var
ôô 
apiResponse
ôô 
=
ôô  !
await
ôô" '
response
ôô( 0
.
ôô0 1
Content
ôô1 8
.
ôô8 9
ReadFromJsonAsync
ôô9 J
<
ôôJ K
ApiResponse
ôôK V
<
ôôV W
CategoryDto
ôôW b
>
ôôb c
>
ôôc d
(
ôôd e
)
ôôe f
;
ôôf g
if
õõ 
(
õõ 
apiResponse
õõ 
?
õõ  
.
õõ  !
Data
õõ! %
!=
õõ& (
null
õõ) -
)
õõ- .
{
úú 
return
ùù 
apiResponse
ùù &
.
ùù& '
Data
ùù' +
;
ùù+ ,
}
ûû 
return
üü 
null
üü 
;
üü 
}
†† 
return
°° 
null
°° 
;
°° 
}
¢¢ 	
catch
££ 
(
££ 
	Exception
££ 
ex
££ 
)
££ 
{
§§ 	
Console
•• 
.
•• 
	WriteLine
•• 
(
•• 
$"
••  
$str
••  2
{
••2 3
ex
••3 5
.
••5 6
Message
••6 =
}
••= >
"
••> ?
)
••? @
;
••@ A
return
¶¶ 
null
¶¶ 
;
¶¶ 
}
ßß 	
}
®® 
public
™™ 

async
™™ 
Task
™™ 
<
™™ 
List
™™ 
<
™™ 
	CourseDto
™™ $
>
™™$ %
>
™™% &&
GetCourseByCategoryAsync
™™' ?
(
™™? @
int
™™@ C

categoryId
™™D N
)
™™N O
{
´´ 
var
¨¨ 
_httpClient
¨¨ 
=
¨¨ 
_factory
¨¨ "
.
¨¨" #
CreateClient
¨¨# /
(
¨¨/ 0
$str
¨¨0 8
)
¨¨8 9
;
¨¨9 :
try
≠≠ 
{
ÆÆ 	
var
ØØ 
response
ØØ 
=
ØØ 
await
ØØ  
_httpClient
ØØ! ,
.
ØØ, -
GetAsync
ØØ- 5
(
ØØ5 6
$"
ØØ6 8
$str
ØØ8 Q
{
ØØQ R

categoryId
ØØR \
}
ØØ\ ]
"
ØØ] ^
)
ØØ^ _
;
ØØ_ `
var
∞∞ 
rawJson
∞∞ 
=
∞∞ 
await
∞∞ 
response
∞∞  (
.
∞∞( )
Content
∞∞) 0
.
∞∞0 1
ReadAsStringAsync
∞∞1 B
(
∞∞B C
)
∞∞C D
;
∞∞D E
Console
±± 
.
±± 
	WriteLine
±± 
(
±± 
$"
±±  
$str
±±  /
{
±±/ 0
rawJson
±±0 7
}
±±7 8
"
±±8 9
)
±±9 :
;
±±: ;
if
≥≥ 
(
≥≥ 
!
≥≥ 
response
≥≥ 
.
≥≥ !
IsSuccessStatusCode
≥≥ -
)
≥≥- .
{
¥¥ 
Console
µµ 
.
µµ 
	WriteLine
µµ !
(
µµ! "
$"
µµ" $
$str
µµ$ :
{
µµ: ;
response
µµ; C
.
µµC D

StatusCode
µµD N
}
µµN O
"
µµO P
)
µµP Q
;
µµQ R
return
∂∂ 
new
∂∂ 
List
∂∂ 
<
∂∂  
	CourseDto
∂∂  )
>
∂∂) *
(
∂∂* +
)
∂∂+ ,
;
∂∂, -
}
∑∑ 
var
ππ 
apiResponse
ππ 
=
ππ 
await
ππ #
response
ππ$ ,
.
ππ, -
Content
ππ- 4
.
ππ4 5
ReadFromJsonAsync
ππ5 F
<
ππF G
ApiResponse
ππG R
<
ππR S
PaginatedResponse
ππS d
<
ππd e
List
ππe i
<
ππi j
	CourseDto
ππj s
>
ππs t
>
ππt u
>
ππu v
>
ππv w
(
ππw x
)
ππx y
;
ππy z
if
ªª 
(
ªª 
apiResponse
ªª 
?
ªª 
.
ªª 
Data
ªª !
?
ªª! "
.
ªª" #
Data
ªª# '
!=
ªª( *
null
ªª+ /
)
ªª/ 0
{
ºº 
return
ΩΩ 
apiResponse
ΩΩ "
.
ΩΩ" #
Data
ΩΩ# '
.
ΩΩ' (
Data
ΩΩ( ,
.
ææ 
Where
ææ 
(
ææ 
c
ææ 
=>
ææ 
c
ææ  !
.
ææ! "
IsActive
ææ" *
&&
ææ+ -
c
ææ. /
.
ææ/ 0

CategoryId
ææ0 :
==
ææ; =

categoryId
ææ> H
)
ææH I
.
øø 
ToList
øø 
(
øø 
)
øø 
;
øø 
}
¿¿ 
return
¬¬ 
new
¬¬ 
List
¬¬ 
<
¬¬ 
	CourseDto
¬¬ %
>
¬¬% &
(
¬¬& '
)
¬¬' (
;
¬¬( )
}
√√ 	
catch
ƒƒ 
(
ƒƒ 
	Exception
ƒƒ 
ex
ƒƒ 
)
ƒƒ 
{
≈≈ 	
Console
∆∆ 
.
∆∆ 
	WriteLine
∆∆ 
(
∆∆ 
$"
∆∆  
$str
∆∆  2
{
∆∆2 3
ex
∆∆3 5
.
∆∆5 6
Message
∆∆6 =
}
∆∆= >
"
∆∆> ?
)
∆∆? @
;
∆∆@ A
return
«« 
new
«« 
List
«« 
<
«« 
	CourseDto
«« %
>
««% &
(
««& '
)
««' (
;
««( )
}
»» 	
}
…… 
}ÀÀ ﬂÊ
SD:\BootcampProject\default-project-main\src\08.BlazorUI\Services\CheckoutService.cs
	namespace 	
MyApp
 
. 
BlazorUI 
. 
Services !
;! "
public 
class 
CheckoutService 
: 
ICheckoutService /
{ 
private		 
readonly		 
IHttpClientFactory		 '
_factory		( 0
;		0 1
public 

CheckoutService 
( 
IHttpClientFactory -
factory. 5
)5 6
{ 
_factory 
= 
factory 
; 
} 
public 

async 
Task 
< 
List 
< 
CartItemResponseDto .
>. /
>/ 0
GetSelfCartItem1 @
(@ A%
AuthenticationHeaderValueA Z
authorization[ h
)h i
{ 
var 
_httpClient 
= 
_factory "
." #
CreateClient# /
(/ 0
$str0 8
)8 9
;9 :
_httpClient 
. !
DefaultRequestHeaders )
.) *
Authorization* 7
=8 9
authorization: G
;G H
try 
{ 	
var 
response 
= 
await  
_httpClient! ,
., -
GetAsync- 5
(5 6
$str6 I
)I J
;J K
if 
( 
response 
. 
IsSuccessStatusCode ,
), -
{ 
var 
apiResponse 
=  !
await" '
response( 0
.0 1
Content1 8
.8 9
ReadFromJsonAsync9 J
<J K
ApiResponseK V
<V W
ListW [
<[ \
CartItemResponseDto\ o
>o p
>p q
>q r
(r s
)s t
;t u
if 
( 
apiResponse 
?  
.  !
Data! %
!=& (
null) -
)- .
{ 
return 
apiResponse &
.& '
Data' +
;+ ,
} 
return 
new 
( 
) 
; 
} 
return   
new   
(   
)   
;   
}!! 	
catch"" 
("" 
	Exception"" 
ex"" 
)"" 
{## 	
Console$$ 
.$$ 
	WriteLine$$ 
($$ 
$"$$  
$str$$  7
{$$7 8
ex$$8 :
.$$: ;
Message$$; B
}$$B C
"$$C D
)$$D E
;$$E F
return%% 
new%% 
(%% 
)%% 
;%% 
}&& 	
}'' 
public(( 

async(( 
Task(( 
<(( 
bool(( 
>((  
AddCourseToCartAsync(( 0
(((0 1%
AuthenticationHeaderValue((1 J
authorization((K X
,((X Y
int((Z ]

scheduleId((^ h
)((h i
{)) 
var** 
_httpClient** 
=** 
_factory** "
.**" #
CreateClient**# /
(**/ 0
$str**0 8
)**8 9
;**9 :
_httpClient++ 
.++ !
DefaultRequestHeaders++ )
.++) *
Authorization++* 7
=++8 9
authorization++: G
;++G H
var,, 
query,, 
=,, 
new,, 

Dictionary,, "
<,," #
string,,# )
,,,) *
string,,+ 1
?,,1 2
>,,2 3
{-- 	
[.. 
$str.. 
].. 
=.. 

scheduleId.. '
...' (
ToString..( 0
(..0 1
)..1 2
}// 	
;//	 

try00 
{11 	
var22 
response22 
=22 
await22  
_httpClient22! ,
.22, -
PutAsync22- 5
(225 6
QueryHelpers226 B
.22B C
AddQueryString22C Q
(22Q R
$str22R e
,22e f
query22g l
)22l m
,22m n
null22o s
)22s t
;22t u
if33 
(33 
response33 
.33 
IsSuccessStatusCode33 ,
)33, -
{44 
var55 
apiResponse55 
=55  !
await55" '
response55( 0
.550 1
Content551 8
.558 9
ReadFromJsonAsync559 J
<55J K
ApiResponse55K V
<55V W
object55W ]
>55] ^
>55^ _
(55_ `
)55` a
;55a b
if77 
(77 
apiResponse77 
!=77  "
null77# '
)77' (
{88 
return99 
true99 
;99  
}:: 
return;; 
false;; 
;;; 
}<< 
return== 
false== 
;== 
}>> 	
catch?? 
(?? 
	Exception?? 
ex?? 
)?? 
{@@ 	
ConsoleAA 
.AA 
	WriteLineAA 
(AA 
$"AA  
$strAA  =
{AA= >
exAA> @
.AA@ A
MessageAAA H
}AAH I
"AAI J
)AAJ K
;AAK L
returnBB 
falseBB 
;BB 
}CC 	
}DD 
publicEE 

asyncEE 
TaskEE 
<EE 
boolEE 
>EE  
RemoveCourseFromCartEE 0
(EE0 1%
AuthenticationHeaderValueEE1 J
authorizationEEK X
,EEX Y
intEEZ ]
cartIdEE^ d
)EEd e
{FF 
varGG 
_httpClientGG 
=GG 
_factoryGG "
.GG" #
CreateClientGG# /
(GG/ 0
$strGG0 8
)GG8 9
;GG9 :
_httpClientHH 
.HH !
DefaultRequestHeadersHH )
.HH) *
AuthorizationHH* 7
=HH8 9
authorizationHH: G
;HHG H
varII 
queryII 
=II 
newII 

DictionaryII "
<II" #
stringII# )
,II) *
stringII+ 1
?II1 2
>II2 3
{JJ 	
[KK 
$strKK 
]KK 
=KK 
cartIdKK 
.KK  
ToStringKK  (
(KK( )
)KK) *
}LL 	
;LL	 

tryMM 
{NN 	
varOO 
responseOO 
=OO 
awaitOO  
_httpClientOO! ,
.OO, -
DeleteAsyncOO- 8
(OO8 9
QueryHelpersOO9 E
.OOE F
AddQueryStringOOF T
(OOT U
$strOOU k
,OOk l
queryOOm r
)OOr s
)OOs t
;OOt u
ifPP 
(PP 
responsePP 
.PP 
IsSuccessStatusCodePP ,
)PP, -
{QQ 
varRR 
apiResponseRR 
=RR  !
awaitRR" '
responseRR( 0
.RR0 1
ContentRR1 8
.RR8 9
ReadFromJsonAsyncRR9 J
<RRJ K
ApiResponseRRK V
<RRV W
objectRRW ]
>RR] ^
>RR^ _
(RR_ `
)RR` a
;RRa b
ifTT 
(TT 
apiResponseTT 
!=TT  "
nullTT# '
)TT' (
{UU 
returnVV 
trueVV 
;VV  
}WW 
returnXX 
falseXX 
;XX 
}YY 
returnZZ 
falseZZ 
;ZZ 
}[[ 	
catch\\ 
(\\ 
	Exception\\ 
ex\\ 
)\\ 
{]] 	
Console^^ 
.^^ 
	WriteLine^^ 
(^^ 
$"^^  
$str^^  =
{^^= >
ex^^> @
.^^@ A
Message^^A H
}^^H I
"^^I J
)^^J K
;^^K L
return__ 
false__ 
;__ 
}`` 	
throwaa 
newaa #
NotImplementedExceptionaa )
(aa) *
)aa* +
;aa+ ,
}bb 
publiccc 

asynccc 
Taskcc 
<cc 
CheckoutResponseDtocc )
?cc) *
>cc* +
CheckoutItemsAsynccc, >
(cc> ?%
AuthenticationHeaderValuecc? X
authorizationccY f
,ccf g
CheckoutRequestDtocch z
request	cc{ Ç
)
ccÇ É
{dd 
varee 
_httpClientee 
=ee 
_factoryee "
.ee" #
CreateClientee# /
(ee/ 0
$stree0 8
)ee8 9
;ee9 :
_httpClientff 
.ff !
DefaultRequestHeadersff )
.ff) *
Authorizationff* 7
=ff8 9
authorizationff: G
;ffG H
trygg 
{hh 	
varii 
responseii 
=ii 
awaitii  
_httpClientii! ,
.ii, -
PostAsJsonAsyncii- <
(ii< =
$strii= T
,iiT U
requestiiV ]
)ii] ^
;ii^ _
ifjj 
(jj 
responsejj 
.jj 
IsSuccessStatusCodejj ,
)jj, -
{kk 
varll 
apiResponsell 
=ll  !
awaitll" '
responsell( 0
.ll0 1
Contentll1 8
.ll8 9
ReadFromJsonAsyncll9 J
<llJ K
ApiResponsellK V
<llV W
CheckoutResponseDtollW j
>llj k
>llk l
(lll m
)llm n
;lln o
ifnn 
(nn 
apiResponsenn 
?nn  
.nn  !
Datann! %
!=nn& (
nullnn) -
)nn- .
{oo 
returnpp 
apiResponsepp &
.pp& '
Datapp' +
;pp+ ,
}qq 
returnrr 
nullrr 
;rr 
}ss 
returntt 
nulltt 
;tt 
}uu 	
catchvv 
(vv 
	Exceptionvv 
exvv 
)vv 
{ww 	
Consolexx 
.xx 
	WriteLinexx 
(xx 
$"xx  
$strxx  ;
{xx; <
exxx< >
.xx> ?
Messagexx? F
}xxF G
"xxG H
)xxH I
;xxI J
returnyy 
nullyy 
;yy 
}zz 	
}{{ 
public|| 

async|| 
Task|| 
<|| 
List|| 
<|| 

PaymentDto|| %
>||% &
>||& '
GetAllPaymentsAsync||( ;
(||; <%
AuthenticationHeaderValue||< U
authorization||V c
)||c d
{}} 
var~~ 
_httpClient~~ 
=~~ 
_factory~~ "
.~~" #
CreateClient~~# /
(~~/ 0
$str~~0 8
)~~8 9
;~~9 :
_httpClient 
. !
DefaultRequestHeaders )
.) *
Authorization* 7
=8 9
authorization: G
;G H
try
ÄÄ 
{
ÅÅ 	
var
ÇÇ 
response
ÇÇ 
=
ÇÇ 
await
ÇÇ  
_httpClient
ÇÇ! ,
.
ÇÇ, -
GetAsync
ÇÇ- 5
(
ÇÇ5 6
$str
ÇÇ6 C
)
ÇÇC D
;
ÇÇD E
if
ÉÉ 
(
ÉÉ 
response
ÉÉ 
.
ÉÉ !
IsSuccessStatusCode
ÉÉ ,
)
ÉÉ, -
{
ÑÑ 
var
ÖÖ 
apiResponse
ÖÖ 
=
ÖÖ  !
await
ÖÖ" '
response
ÖÖ( 0
.
ÖÖ0 1
Content
ÖÖ1 8
.
ÖÖ8 9
ReadFromJsonAsync
ÖÖ9 J
<
ÖÖJ K
ApiResponse
ÖÖK V
<
ÖÖV W
List
ÖÖW [
<
ÖÖ[ \

PaymentDto
ÖÖ\ f
>
ÖÖf g
>
ÖÖg h
>
ÖÖh i
(
ÖÖi j
)
ÖÖj k
;
ÖÖk l
if
áá 
(
áá 
apiResponse
áá 
?
áá  
.
áá  !
Data
áá! %
!=
áá& (
null
áá) -
)
áá- .
{
àà 
return
ââ 
apiResponse
ââ &
.
ââ& '
Data
ââ' +
;
ââ+ ,
}
ää 
return
ãã 
new
ãã 
(
ãã 
)
ãã 
;
ãã 
}
åå 
return
çç 
new
çç 
(
çç 
)
çç 
;
çç 
}
éé 	
catch
èè 
(
èè 
	Exception
èè 
ex
èè 
)
èè 
{
êê 	
Console
ëë 
.
ëë 
	WriteLine
ëë 
(
ëë 
$"
ëë  
$str
ëë  7
{
ëë7 8
ex
ëë8 :
.
ëë: ;
Message
ëë; B
}
ëëB C
"
ëëC D
)
ëëD E
;
ëëE F
return
íí 
new
íí 
(
íí 
)
íí 
;
íí 
}
ìì 	
}
îî 
public
ññ 

async
ññ 
Task
ññ 
<
ññ 
List
ññ 
<
ññ 

InvoiceDto
ññ %
>
ññ% &
>
ññ& '%
GetAllInvoiceAdminAsync
ññ( ?
(
ññ? @'
AuthenticationHeaderValue
ññ@ Y
authorization
ññZ g
)
ññg h
{
óó 
var
òò 
_httpClient
òò 
=
òò 
_factory
òò "
.
òò" #
CreateClient
òò# /
(
òò/ 0
$str
òò0 8
)
òò8 9
;
òò9 :
_httpClient
ôô 
.
ôô #
DefaultRequestHeaders
ôô )
.
ôô) *
Authorization
ôô* 7
=
ôô8 9
authorization
ôô: G
;
ôôG H
try
öö 
{
õõ 	
var
úú 
response
úú 
=
úú 
await
úú  
_httpClient
úú! ,
.
úú, -
GetAsync
úú- 5
(
úú5 6
$str
úú6 I
)
úúI J
;
úúJ K
if
ùù 
(
ùù 
response
ùù 
.
ùù !
IsSuccessStatusCode
ùù ,
)
ùù, -
{
ûû 
var
üü 
apiResponse
üü 
=
üü  !
await
üü" '
response
üü( 0
.
üü0 1
Content
üü1 8
.
üü8 9
ReadFromJsonAsync
üü9 J
<
üüJ K
ApiResponse
üüK V
<
üüV W
IEnumerable
üüW b
<
üüb c

InvoiceDto
üüc m
>
üüm n
>
üün o
>
üüo p
(
üüp q
)
üüq r
;
üür s
if
°° 
(
°° 
apiResponse
°° 
?
°°  
.
°°  !
Data
°°! %
!=
°°& (
null
°°) -
)
°°- .
{
¢¢ 
return
££ 
apiResponse
££ &
.
££& '
Data
££' +
.
££+ ,
ToList
££, 2
(
££2 3
)
££3 4
;
££4 5
}
§§ 
return
•• 
new
•• 
(
•• 
)
•• 
;
•• 
}
¶¶ 
return
ßß 
new
ßß 
(
ßß 
)
ßß 
;
ßß 
}
®® 	
catch
©© 
(
©© 
	Exception
©© 
ex
©© 
)
©© 
{
™™ 	
Console
´´ 
.
´´ 
	WriteLine
´´ 
(
´´ 
$"
´´  
$str
´´  6
{
´´6 7
ex
´´7 9
.
´´9 :
Message
´´: A
}
´´A B
"
´´B C
)
´´C D
;
´´D E
return
¨¨ 
new
¨¨ 
(
¨¨ 
)
¨¨ 
;
¨¨ 
}
≠≠ 	
}
ÆÆ 
public
ØØ 

async
ØØ 
Task
ØØ 
<
ØØ 
List
ØØ 
<
ØØ 

InvoiceDto
ØØ %
>
ØØ% &
>
ØØ& '"
GetSelfInvoicesAsync
ØØ( <
(
ØØ< ='
AuthenticationHeaderValue
ØØ= V
authorization
ØØW d
)
ØØd e
{
∞∞ 
var
±± 
_httpClient
±± 
=
±± 
_factory
±± "
.
±±" #
CreateClient
±±# /
(
±±/ 0
$str
±±0 8
)
±±8 9
;
±±9 :
_httpClient
≤≤ 
.
≤≤ #
DefaultRequestHeaders
≤≤ )
.
≤≤) *
Authorization
≤≤* 7
=
≤≤8 9
authorization
≤≤: G
;
≤≤G H
try
≥≥ 
{
¥¥ 	
var
µµ 
response
µµ 
=
µµ 
await
µµ  
_httpClient
µµ! ,
.
µµ, -
GetAsync
µµ- 5
(
µµ5 6
$str
µµ6 C
)
µµC D
;
µµD E
if
∂∂ 
(
∂∂ 
response
∂∂ 
.
∂∂ !
IsSuccessStatusCode
∂∂ ,
)
∂∂, -
{
∑∑ 
var
∏∏ 
apiResponse
∏∏ 
=
∏∏  !
await
∏∏" '
response
∏∏( 0
.
∏∏0 1
Content
∏∏1 8
.
∏∏8 9
ReadFromJsonAsync
∏∏9 J
<
∏∏J K
ApiResponse
∏∏K V
<
∏∏V W
IEnumerable
∏∏W b
<
∏∏b c

InvoiceDto
∏∏c m
>
∏∏m n
>
∏∏n o
>
∏∏o p
(
∏∏p q
)
∏∏q r
;
∏∏r s
if
∫∫ 
(
∫∫ 
apiResponse
∫∫ 
?
∫∫  
.
∫∫  !
Data
∫∫! %
!=
∫∫& (
null
∫∫) -
)
∫∫- .
{
ªª 
return
ºº 
apiResponse
ºº &
.
ºº& '
Data
ºº' +
.
ΩΩ 
OrderByDescending
ΩΩ *
(
ΩΩ* +
i
ΩΩ+ ,
=>
ΩΩ- /
i
ΩΩ0 1
.
ΩΩ1 2
	CreatedAt
ΩΩ2 ;
)
ΩΩ; <
.
ææ 
ToList
ææ 
(
ææ  
)
ææ  !
;
ææ! "
}
øø 
return
¿¿ 
new
¿¿ 
(
¿¿ 
)
¿¿ 
;
¿¿ 
}
¡¡ 
return
¬¬ 
new
¬¬ 
(
¬¬ 
)
¬¬ 
;
¬¬ 
}
√√ 	
catch
ƒƒ 
(
ƒƒ 
	Exception
ƒƒ 
ex
ƒƒ 
)
ƒƒ 
{
≈≈ 	
Console
∆∆ 
.
∆∆ 
	WriteLine
∆∆ 
(
∆∆ 
$"
∆∆  
$str
∆∆  <
{
∆∆< =
ex
∆∆= ?
.
∆∆? @
Message
∆∆@ G
}
∆∆G H
"
∆∆H I
)
∆∆I J
;
∆∆J K
return
«« 
new
«« 
(
«« 
)
«« 
;
«« 
}
»» 	
}
…… 
public
ÀÀ 

async
ÀÀ 
Task
ÀÀ 
<
ÀÀ 

InvoiceDto
ÀÀ  
?
ÀÀ  !
>
ÀÀ! "&
GetInvoiceByIdAdminAsync
ÀÀ# ;
(
ÀÀ; <'
AuthenticationHeaderValue
ÀÀ< U
authorization
ÀÀV c
,
ÀÀc d
int
ÀÀe h
	invoiceId
ÀÀi r
)
ÀÀr s
{
ÃÃ 
var
ÕÕ 
_httpClient
ÕÕ 
=
ÕÕ 
_factory
ÕÕ "
.
ÕÕ" #
CreateClient
ÕÕ# /
(
ÕÕ/ 0
$str
ÕÕ0 8
)
ÕÕ8 9
;
ÕÕ9 :
_httpClient
ŒŒ 
.
ŒŒ #
DefaultRequestHeaders
ŒŒ )
.
ŒŒ) *
Authorization
ŒŒ* 7
=
ŒŒ8 9
authorization
ŒŒ: G
;
ŒŒG H
try
œœ 
{
–– 	
var
—— 
response
—— 
=
—— 
await
——  
_httpClient
——! ,
.
——, -
GetAsync
——- 5
(
——5 6
$"
——6 8
$str
——8 J
{
——J K
	invoiceId
——K T
}
——T U
"
——U V
)
——V W
;
——W X
if
““ 
(
““ 
response
““ 
.
““ !
IsSuccessStatusCode
““ ,
)
““, -
{
”” 
var
‘‘ 
apiResponse
‘‘ 
=
‘‘  !
await
‘‘" '
response
‘‘( 0
.
‘‘0 1
Content
‘‘1 8
.
‘‘8 9
ReadFromJsonAsync
‘‘9 J
<
‘‘J K
ApiResponse
‘‘K V
<
‘‘V W

InvoiceDto
‘‘W a
>
‘‘a b
>
‘‘b c
(
‘‘c d
)
‘‘d e
;
‘‘e f
if
÷÷ 
(
÷÷ 
apiResponse
÷÷ 
?
÷÷  
.
÷÷  !
Data
÷÷! %
!=
÷÷& (
null
÷÷) -
)
÷÷- .
{
◊◊ 
return
ÿÿ 
apiResponse
ÿÿ &
.
ÿÿ& '
Data
ÿÿ' +
;
ÿÿ+ ,
}
ŸŸ 
return
⁄⁄ 
null
⁄⁄ 
;
⁄⁄ 
}
€€ 
return
‹‹ 
null
‹‹ 
;
‹‹ 
}
›› 	
catch
ﬁﬁ 
(
ﬁﬁ 
	Exception
ﬁﬁ 
ex
ﬁﬁ 
)
ﬁﬁ 
{
ﬂﬂ 	
Console
‡‡ 
.
‡‡ 
	WriteLine
‡‡ 
(
‡‡ 
$"
‡‡  
$str
‡‡  ;
{
‡‡; <
ex
‡‡< >
.
‡‡> ?
Message
‡‡? F
}
‡‡F G
"
‡‡G H
)
‡‡H I
;
‡‡I J
return
·· 
null
·· 
;
·· 
}
‚‚ 	
}
„„ 
public
ÂÂ 

async
ÂÂ 
Task
ÂÂ 
<
ÂÂ 

InvoiceDto
ÂÂ  
?
ÂÂ  !
>
ÂÂ! "!
GetInvoiceByIdAsync
ÂÂ# 6
(
ÂÂ6 7'
AuthenticationHeaderValue
ÂÂ7 P
authorization
ÂÂQ ^
,
ÂÂ^ _
int
ÂÂ` c
	invoiceId
ÂÂd m
)
ÂÂm n
{
ÊÊ 
var
ÁÁ 
_httpClient
ÁÁ 
=
ÁÁ 
_factory
ÁÁ "
.
ÁÁ" #
CreateClient
ÁÁ# /
(
ÁÁ/ 0
$str
ÁÁ0 8
)
ÁÁ8 9
;
ÁÁ9 :
_httpClient
ËË 
.
ËË #
DefaultRequestHeaders
ËË )
.
ËË) *
Authorization
ËË* 7
=
ËË8 9
authorization
ËË: G
;
ËËG H
try
ÈÈ 
{
ÍÍ 	
var
ÎÎ 
response
ÎÎ 
=
ÎÎ 
await
ÎÎ  
_httpClient
ÎÎ! ,
.
ÎÎ, -
GetAsync
ÎÎ- 5
(
ÎÎ5 6
$"
ÎÎ6 8
$str
ÎÎ8 D
{
ÎÎD E
	invoiceId
ÎÎE N
}
ÎÎN O
"
ÎÎO P
)
ÎÎP Q
;
ÎÎQ R
if
ÏÏ 
(
ÏÏ 
response
ÏÏ 
.
ÏÏ !
IsSuccessStatusCode
ÏÏ ,
)
ÏÏ, -
{
ÌÌ 
var
ÓÓ 
apiResponse
ÓÓ 
=
ÓÓ  !
await
ÓÓ" '
response
ÓÓ( 0
.
ÓÓ0 1
Content
ÓÓ1 8
.
ÓÓ8 9
ReadFromJsonAsync
ÓÓ9 J
<
ÓÓJ K
ApiResponse
ÓÓK V
<
ÓÓV W

InvoiceDto
ÓÓW a
>
ÓÓa b
>
ÓÓb c
(
ÓÓc d
)
ÓÓd e
;
ÓÓe f
if
 
(
 
apiResponse
 
?
  
.
  !
Data
! %
!=
& (
null
) -
)
- .
{
ÒÒ 
return
ÚÚ 
apiResponse
ÚÚ &
.
ÚÚ& '
Data
ÚÚ' +
;
ÚÚ+ ,
}
ÛÛ 
return
ÙÙ 
null
ÙÙ 
;
ÙÙ 
}
ıı 
return
ˆˆ 
null
ˆˆ 
;
ˆˆ 
}
˜˜ 	
catch
¯¯ 
(
¯¯ 
	Exception
¯¯ 
ex
¯¯ 
)
¯¯ 
{
˘˘ 	
Console
˙˙ 
.
˙˙ 
	WriteLine
˙˙ 
(
˙˙ 
$"
˙˙  
$str
˙˙  ;
{
˙˙; <
ex
˙˙< >
.
˙˙> ?
Message
˙˙? F
}
˙˙F G
"
˙˙G H
)
˙˙H I
;
˙˙I J
return
˚˚ 
null
˚˚ 
;
˚˚ 
}
¸¸ 	
}
˝˝ 
public
ˇˇ 

async
ˇˇ 
Task
ˇˇ 
<
ˇˇ 
List
ˇˇ 
<
ˇˇ 
InvoiceDetailDto
ˇˇ +
>
ˇˇ+ ,
>
ˇˇ, -4
&GetInvoiceDetailsByInvoiceIdAdminAsync
ˇˇ. T
(
ˇˇT U'
AuthenticationHeaderValue
ˇˇU n
authorization
ˇˇo |
,
ˇˇ| }
intˇˇ~ Å
	invoiceIdˇˇÇ ã
)ˇˇã å
{
ÄÄ 
var
ÅÅ 
_httpClient
ÅÅ 
=
ÅÅ 
_factory
ÅÅ "
.
ÅÅ" #
CreateClient
ÅÅ# /
(
ÅÅ/ 0
$str
ÅÅ0 8
)
ÅÅ8 9
;
ÅÅ9 :
_httpClient
ÇÇ 
.
ÇÇ #
DefaultRequestHeaders
ÇÇ )
.
ÇÇ) *
Authorization
ÇÇ* 7
=
ÇÇ8 9
authorization
ÇÇ: G
;
ÇÇG H
try
ÉÉ 
{
ÑÑ 	
var
ÖÖ 
response
ÖÖ 
=
ÖÖ 
await
ÖÖ  
_httpClient
ÖÖ! ,
.
ÖÖ, -
GetAsync
ÖÖ- 5
(
ÖÖ5 6
$"
ÖÖ6 8
$str
ÖÖ8 P
{
ÖÖP Q
	invoiceId
ÖÖQ Z
}
ÖÖZ [
"
ÖÖ[ \
)
ÖÖ\ ]
;
ÖÖ] ^
if
ÜÜ 
(
ÜÜ 
response
ÜÜ 
.
ÜÜ !
IsSuccessStatusCode
ÜÜ ,
)
ÜÜ, -
{
áá 
var
àà 
apiResponse
àà 
=
àà  !
await
àà" '
response
àà( 0
.
àà0 1
Content
àà1 8
.
àà8 9
ReadFromJsonAsync
àà9 J
<
ààJ K
ApiResponse
ààK V
<
ààV W
IEnumerable
ààW b
<
ààb c
InvoiceDetailDto
ààc s
>
ààs t
>
ààt u
>
ààu v
(
ààv w
)
ààw x
;
ààx y
if
ää 
(
ää 
apiResponse
ää 
?
ää  
.
ää  !
Data
ää! %
!=
ää& (
null
ää) -
)
ää- .
{
ãã 
return
åå 
apiResponse
åå &
.
åå& '
Data
åå' +
.
åå+ ,
ToList
åå, 2
(
åå2 3
)
åå3 4
;
åå4 5
}
çç 
return
éé 
new
éé 
(
éé 
)
éé 
;
éé 
}
èè 
return
êê 
new
êê 
(
êê 
)
êê 
;
êê 
}
ëë 	
catch
íí 
(
íí 
	Exception
íí 
ex
íí 
)
íí 
{
ìì 	
Console
îî 
.
îî 
	WriteLine
îî 
(
îî 
$"
îî  
$str
îî  6
{
îî6 7
ex
îî7 9
.
îî9 :
Message
îî: A
}
îîA B
"
îîB C
)
îîC D
;
îîD E
return
ïï 
new
ïï 
(
ïï 
)
ïï 
;
ïï 
}
ññ 	
}
óó 
public
òò 

async
òò 
Task
òò 
<
òò 
List
òò 
<
òò 
InvoiceDetailDto
òò +
>
òò+ ,
>
òò, -/
!GetInvoiceDetailsByInvoiceIdAsync
òò. O
(
òòO P'
AuthenticationHeaderValue
òòP i
authorization
òòj w
,
òòw x
int
òòy |
	invoiceIdòò} Ü
)òòÜ á
{
ôô 
var
öö 
_httpClient
öö 
=
öö 
_factory
öö "
.
öö" #
CreateClient
öö# /
(
öö/ 0
$str
öö0 8
)
öö8 9
;
öö9 :
_httpClient
õõ 
.
õõ #
DefaultRequestHeaders
õõ )
.
õõ) *
Authorization
õõ* 7
=
õõ8 9
authorization
õõ: G
;
õõG H
try
úú 
{
ùù 	
var
ûû 
response
ûû 
=
ûû 
await
ûû  
_httpClient
ûû! ,
.
ûû, -
GetAsync
ûû- 5
(
ûû5 6
$"
ûû6 8
$str
ûû8 J
{
ûûJ K
	invoiceId
ûûK T
}
ûûT U
"
ûûU V
)
ûûV W
;
ûûW X
if
üü 
(
üü 
response
üü 
.
üü !
IsSuccessStatusCode
üü ,
)
üü, -
{
†† 
var
°° 
apiResponse
°° 
=
°°  !
await
°°" '
response
°°( 0
.
°°0 1
Content
°°1 8
.
°°8 9
ReadFromJsonAsync
°°9 J
<
°°J K
ApiResponse
°°K V
<
°°V W
IEnumerable
°°W b
<
°°b c
InvoiceDetailDto
°°c s
>
°°s t
>
°°t u
>
°°u v
(
°°v w
)
°°w x
;
°°x y
if
££ 
(
££ 
apiResponse
££ 
?
££  
.
££  !
Data
££! %
!=
££& (
null
££) -
)
££- .
{
§§ 
return
•• 
apiResponse
•• &
.
••& '
Data
••' +
.
••+ ,
ToList
••, 2
(
••2 3
)
••3 4
;
••4 5
}
¶¶ 
return
ßß 
new
ßß 
(
ßß 
)
ßß 
;
ßß 
}
®® 
return
©© 
new
©© 
(
©© 
)
©© 
;
©© 
}
™™ 	
catch
´´ 
(
´´ 
	Exception
´´ 
ex
´´ 
)
´´ 
{
¨¨ 	
Console
≠≠ 
.
≠≠ 
	WriteLine
≠≠ 
(
≠≠ 
$"
≠≠  
$str
≠≠  <
{
≠≠< =
ex
≠≠= ?
.
≠≠? @
Message
≠≠@ G
}
≠≠G H
"
≠≠H I
)
≠≠I J
;
≠≠J K
return
ÆÆ 
new
ÆÆ 
(
ÆÆ 
)
ÆÆ 
;
ÆÆ 
}
ØØ 	
}
∞∞ 
}±± ’”
OD:\BootcampProject\default-project-main\src\08.BlazorUI\Services\AuthService.cs
	namespace 	
MyApp
 
. 
BlazorUI 
. 
Services !
;! "
public

 
class

 
AuthService

 
:

 
IAuthService

 '
{ 
private 
readonly 
IHttpClientFactory '
_factory( 0
;0 1
private 
readonly  
ILocalStorageService )
_localStorage* 7
;7 8
private 
readonly '
AuthenticationStateProvider 0
_authStateProvider1 C
;C D
private 
readonly #
JwtSecurityTokenHandler ,$
_jwtSecurityTokenHandler- E
=F G
newH K
(K L
)L M
;M N
public 

AuthService 
( 
IHttpClientFactory 
factory "
," # 
ILocalStorageService 
localStorage )
,) *'
AuthenticationStateProvider #
authStateProvider$ 5
)5 6
{ 
_factory 
= 
factory 
; 
_localStorage 
= 
localStorage $
;$ %
_authStateProvider 
= 
authStateProvider .
;. /
} 
public 

async 
Task 
< 
AuthResponseDto %
?% &
>& '

LoginAsync( 2
(2 3
LoginRequestDto3 B
requestC J
)J K
{ 
var 
_httpClient 
= 
_factory "
." #
CreateClient# /
(/ 0
$str0 8
)8 9
;9 :
try 
{ 	
var 
response 
= 
await  
_httpClient! ,
., -
PostAsJsonAsync- <
(< =
$str= M
,M N
requestO V
)V W
;W X
if!! 
(!! 
response!! 
.!! 
IsSuccessStatusCode!! ,
)!!, -
{"" 
var## 
apiResponse## 
=##  !
await##" '
response##( 0
.##0 1
Content##1 8
.##8 9
ReadFromJsonAsync##9 J
<##J K
ApiResponse##K V
<##V W
AuthResponseDto##W f
>##f g
>##g h
(##h i
)##i j
;##j k
if%% 
(%% 
apiResponse%% 
?%%  
.%%  !
Data%%! %
!=%%& (
null%%) -
)%%- .
{&& 
await'' 
SetTokensAsync'' (
(''( )
apiResponse'') 4
.''4 5
Data''5 9
.''9 :
AccessToken'': E
,''E F
apiResponse''G R
.''R S
Data''S W
.''W X
RefreshToken''X d
)''d e
;''e f
_httpClient)) 
.))  !
DefaultRequestHeaders))  5
.))5 6
Authorization))6 C
=))D E
new** %
AuthenticationHeaderValue** 5
(**5 6
$str**6 >
,**> ?
apiResponse**@ K
.**K L
Data**L P
.**P Q
AccessToken**Q \
)**\ ]
;**] ^
(,, 
(,, #
CustomAuthStateProvider,, -
),,- .
_authStateProvider,,. @
),,@ A
.,,A B$
NotifyUserAuthentication,,B Z
(,,Z [
apiResponse,,[ f
.,,f g
Data,,g k
.,,k l
AccessToken,,l w
),,w x
;,,x y
return.. 
apiResponse.. &
...& '
Data..' +
;..+ ,
}// 
}00 
return22 
null22 
;22 
}33 	
catch44 
{55 	
return66 
null66 
;66 
}77 	
}88 
public99 

async99 
Task99 
<99 
bool99 
>99 
RegisterAsync99 )
(99) *
RegisterRequestDto99* <
request99= D
)99D E
{:: 
var;; 
_httpClient;; 
=;; 
_factory;; "
.;;" #
CreateClient;;# /
(;;/ 0
$str;;0 8
);;8 9
;;;9 :
try<< 
{== 	
var>> 
response>> 
=>> 
await>>  
_httpClient>>! ,
.>>, -
PostAsJsonAsync>>- <
(>>< =
$str>>= P
,>>P Q
request>>R Y
)>>Y Z
;>>Z [
if?? 
(?? 
!?? 
response?? 
.?? 
IsSuccessStatusCode?? -
)??- .
{@@ 
varAA 
problemDetailsAA "
=AA# $
awaitAA% *
responseAA+ 3
.AA3 4
ContentAA4 ;
.AA; <
ReadFromJsonAsyncAA< M
<AAM N
ProblemDetailsAAN \
>AA\ ]
(AA] ^
)AA^ _
;AA_ `
ifBB 
(BB 
problemDetailsBB "
==BB# %
nullBB& *
)BB* +
{CC 
returnEE 
falseEE  
;EE  !
}FF 
returnGG 
falseGG 
;GG 
}HH 
varII 
apiResponseII 
=II 
awaitII #
responseII$ ,
.II, -
ContentII- 4
.II4 5
ReadFromJsonAsyncII5 F
<IIF G
ApiResponseIIG R
<IIR S
objectIIS Y
>IIY Z
>IIZ [
(II[ \
)II\ ]
;II] ^
returnJJ 
apiResponseJJ 
!=JJ !
nullJJ" &
;JJ& '
}KK 	
catchLL 
{MM 	
returnNN 
falseNN 
;NN 
}OO 	
}PP 
publicQQ 

asyncQQ 
TaskQQ 
<QQ 
boolQQ 
>QQ 
ConfirmEmailAsyncQQ -
(QQ- ."
ConfirmEmailRequestDtoQQ. D
requestQQE L
)QQL M
{RR 
varSS 
_httpClientSS 
=SS 
_factorySS "
.SS" #
CreateClientSS# /
(SS/ 0
$strSS0 8
)SS8 9
;SS9 :
tryTT 
{UU 	
varVV 
responseVV 
=VV 
awaitVV  
_httpClientVV! ,
.VV, -
PostAsJsonAsyncVV- <
(VV< =
$strVV= U
,VVU V
requestVVW ^
)VV^ _
;VV_ `
ifXX 
(XX 
!XX 
responseXX 
.XX 
IsSuccessStatusCodeXX -
)XX- .
{YY 
returnZZ 
falseZZ 
;ZZ 
}[[ 
var\\ 
apiResponse\\ 
=\\ 
await\\ #
response\\$ ,
.\\, -
Content\\- 4
.\\4 5
ReadFromJsonAsync\\5 F
<\\F G
ApiResponse\\G R
<\\R S
object\\S Y
>\\Y Z
>\\Z [
(\\[ \
)\\\ ]
;\\] ^
return]] 
apiResponse]] 
!=]] !
null]]" &
;]]& '
}^^ 	
catch__ 
{`` 	
returnaa 
falseaa 
;aa 
}bb 	
}cc 
publicdd 

asyncdd 
Taskdd 
<dd 
booldd 
>dd 
ChangePasswordAsyncdd /
(dd/ 0%
AuthenticationHeaderValuedd0 I
authorizationddJ W
,ddW X$
ChangePasswordRequestDtoddY q
requestddr y
)ddy z
{ee 
varff 
_httpClientff 
=ff 
_factoryff "
.ff" #
CreateClientff# /
(ff/ 0
$strff0 8
)ff8 9
;ff9 :
_httpClientgg 
.gg !
DefaultRequestHeadersgg )
.gg) *
Authorizationgg* 7
=gg8 9
authorizationgg: G
;ggG H
tryhh 
{ii 	
varjj 
responsejj 
=jj 
awaitjj  
_httpClientjj! ,
.jj, -
PostAsJsonAsyncjj- <
(jj< =
$strjj= W
,jjW X
requestjjY `
)jj` a
;jja b
ifll 
(ll 
!ll 
responsell 
.ll 
IsSuccessStatusCodell -
)ll- .
{mm 
returnnn 
falsenn 
;nn 
}oo 
varpp 
apiResponsepp 
=pp 
awaitpp #
responsepp$ ,
.pp, -
Contentpp- 4
.pp4 5
ReadFromJsonAsyncpp5 F
<ppF G
ApiResponseppG R
<ppR S
objectppS Y
>ppY Z
>ppZ [
(pp[ \
)pp\ ]
;pp] ^
returnqq 
apiResponseqq 
!=qq !
nullqq" &
;qq& '
}rr 	
catchss 
{tt 	
returnuu 
falseuu 
;uu 
}vv 	
}ww 
publicxx 

asyncxx 
Taskxx 
<xx 
boolxx 
>xx 
ForgotPasswordAsyncxx /
(xx/ 0$
ForgotPasswordRequestDtoxx0 H
requestxxI P
)xxP Q
{yy 
varzz 
_httpClientzz 
=zz 
_factoryzz "
.zz" #
CreateClientzz# /
(zz/ 0
$strzz0 8
)zz8 9
;zz9 :
try{{ 
{|| 	
var}} 
response}} 
=}} 
await}}  
_httpClient}}! ,
.}}, -
PostAsJsonAsync}}- <
(}}< =
$str}}= W
,}}W X
request}}Y `
)}}` a
;}}a b
if 
( 
! 
response 
. 
IsSuccessStatusCode -
)- .
{
ÄÄ 
return
ÅÅ 
false
ÅÅ 
;
ÅÅ 
}
ÇÇ 
var
ÉÉ 
apiResponse
ÉÉ 
=
ÉÉ 
await
ÉÉ #
response
ÉÉ$ ,
.
ÉÉ, -
Content
ÉÉ- 4
.
ÉÉ4 5
ReadFromJsonAsync
ÉÉ5 F
<
ÉÉF G
ApiResponse
ÉÉG R
<
ÉÉR S
object
ÉÉS Y
>
ÉÉY Z
>
ÉÉZ [
(
ÉÉ[ \
)
ÉÉ\ ]
;
ÉÉ] ^
return
ÑÑ 
apiResponse
ÑÑ 
!=
ÑÑ !
null
ÑÑ" &
;
ÑÑ& '
}
ÖÖ 	
catch
ÜÜ 
{
áá 	
return
àà 
false
àà 
;
àà 
}
ââ 	
}
ää 
public
ãã 

async
ãã 
Task
ãã 
<
ãã 
bool
ãã 
>
ãã  
ResetPasswordAsync
ãã .
(
ãã. /%
ResetPasswordRequestDto
ãã/ F
request
ããG N
)
ããN O
{
åå 
var
çç 
_httpClient
çç 
=
çç 
_factory
çç "
.
çç" #
CreateClient
çç# /
(
çç/ 0
$str
çç0 8
)
çç8 9
;
çç9 :
try
éé 
{
èè 	
var
êê 
response
êê 
=
êê 
await
êê  
_httpClient
êê! ,
.
êê, -
PostAsJsonAsync
êê- <
(
êê< =
$str
êê= V
,
êêV W
request
êêX _
)
êê_ `
;
êê` a
if
íí 
(
íí 
!
íí 
response
íí 
.
íí !
IsSuccessStatusCode
íí -
)
íí- .
{
ìì 
return
îî 
false
îî 
;
îî 
}
ïï 
var
ññ 
apiResponse
ññ 
=
ññ 
await
ññ #
response
ññ$ ,
.
ññ, -
Content
ññ- 4
.
ññ4 5
ReadFromJsonAsync
ññ5 F
<
ññF G
ApiResponse
ññG R
<
ññR S
object
ññS Y
>
ññY Z
>
ññZ [
(
ññ[ \
)
ññ\ ]
;
ññ] ^
return
óó 
apiResponse
óó 
!=
óó !
null
óó" &
;
óó& '
}
òò 	
catch
ôô 
{
öö 	
return
õõ 
false
õõ 
;
õõ 
}
úú 	
}
ùù 
public
ûû 

async
ûû 
Task
ûû 
<
ûû 
UserDto
ûû 
?
ûû 
>
ûû !
GetCurrentUserAsync
ûû  3
(
ûû3 4'
AuthenticationHeaderValue
ûû4 M
authorization
ûûN [
)
ûû[ \
{
üü 
var
†† 
_httpClient
†† 
=
†† 
_factory
†† "
.
††" #
CreateClient
††# /
(
††/ 0
$str
††0 8
)
††8 9
;
††9 :
_httpClient
°° 
.
°° #
DefaultRequestHeaders
°° )
.
°°) *
Authorization
°°* 7
=
°°8 9
authorization
°°: G
;
°°G H
try
¢¢ 
{
££ 	
var
§§ 
response
§§ 
=
§§ 
await
§§  
_httpClient
§§! ,
.
§§, -
GetAsync
§§- 5
(
§§5 6
$str
§§6 C
)
§§C D
;
§§D E
if
¶¶ 
(
¶¶ 
response
¶¶ 
.
¶¶ !
IsSuccessStatusCode
¶¶ ,
)
¶¶, -
{
ßß 
var
®® 
apiResponse
®® 
=
®®  !
await
®®" '
response
®®( 0
.
®®0 1
Content
®®1 8
.
®®8 9
ReadFromJsonAsync
®®9 J
<
®®J K
ApiResponse
®®K V
<
®®V W
UserDto
®®W ^
>
®®^ _
>
®®_ `
(
®®` a
)
®®a b
;
®®b c
return
©© 
apiResponse
©© "
?
©©" #
.
©©# $
Data
©©$ (
;
©©( )
}
™™ 
return
¨¨ 
null
¨¨ 
;
¨¨ 
}
≠≠ 	
catch
ÆÆ 
{
ØØ 	
return
∞∞ 
null
∞∞ 
;
∞∞ 
}
±± 	
}
≤≤ 
public
≥≥ 

async
≥≥ 
Task
≥≥ 
LogoutAsync
≥≥ !
(
≥≥! "'
AuthenticationHeaderValue
≥≥" ;
authorization
≥≥< I
)
≥≥I J
{
¥¥ 
var
µµ 
_httpClient
µµ 
=
µµ 
_factory
µµ "
.
µµ" #
CreateClient
µµ# /
(
µµ/ 0
$str
µµ0 8
)
µµ8 9
;
µµ9 :
_httpClient
∂∂ 
.
∂∂ #
DefaultRequestHeaders
∂∂ )
.
∂∂) *
Authorization
∂∂* 7
=
∂∂8 9
authorization
∂∂: G
;
∂∂G H
try
∑∑ 
{
∏∏ 	
var
ππ 
response
ππ 
=
ππ 
await
ππ  
_httpClient
ππ! ,
.
ππ, -
	PostAsync
ππ- 6
(
ππ6 7
$str
ππ7 H
,
ππH I
null
ππJ N
)
ππN O
;
ππO P
if
ªª 
(
ªª 
!
ªª 
response
ªª 
.
ªª !
IsSuccessStatusCode
ªª -
)
ªª- .
{
ºº 
await
ææ 
ClearTokensAsync
ææ &
(
ææ& '
)
ææ' (
;
ææ( )
(
¿¿ 
(
¿¿ %
CustomAuthStateProvider
¿¿ )
)
¿¿) * 
_authStateProvider
¿¿* <
)
¿¿< =
.
¿¿= >
NotifyUserLogout
¿¿> N
(
¿¿N O
)
¿¿O P
;
¿¿P Q
return
¡¡ 
;
¡¡ 
}
¬¬ 
var
√√ 
apiResponse
√√ 
=
√√ 
await
√√ #
response
√√$ ,
.
√√, -
Content
√√- 4
.
√√4 5
ReadFromJsonAsync
√√5 F
<
√√F G
ApiResponse
√√G R
<
√√R S
UserDto
√√S Z
>
√√Z [
>
√√[ \
(
√√\ ]
)
√√] ^
;
√√^ _
if
ƒƒ 
(
ƒƒ 
apiResponse
ƒƒ 
==
ƒƒ 
null
ƒƒ #
)
ƒƒ# $
{
≈≈ 
await
«« 
ClearTokensAsync
«« &
(
««& '
)
««' (
;
««( )
(
…… 
(
…… %
CustomAuthStateProvider
…… )
)
……) * 
_authStateProvider
……* <
)
……< =
.
……= >
NotifyUserLogout
……> N
(
……N O
)
……O P
;
……P Q
return
   
;
   
}
ÀÀ 
await
ÕÕ 
ClearTokensAsync
ÕÕ "
(
ÕÕ" #
)
ÕÕ# $
;
ÕÕ$ %
(
œœ 
(
œœ %
CustomAuthStateProvider
œœ %
)
œœ% & 
_authStateProvider
œœ& 8
)
œœ8 9
.
œœ9 :
NotifyUserLogout
œœ: J
(
œœJ K
)
œœK L
;
œœL M
return
–– 
;
–– 
}
—— 	
catch
““ 
{
”” 	
return
’’ 
;
’’ 
}
÷÷ 	
}
ÿÿ 
public
ŸŸ 

async
ŸŸ 
Task
ŸŸ 
<
ŸŸ 
bool
ŸŸ 
>
ŸŸ 
IsLoggedInAsync
ŸŸ +
(
ŸŸ+ ,
)
ŸŸ, -
{
⁄⁄ 
var
€€ 
_httpClient
€€ 
=
€€ 
_factory
€€ "
.
€€" #
CreateClient
€€# /
(
€€/ 0
$str
€€0 8
)
€€8 9
;
€€9 :
if
›› 

(
›› 
_httpClient
›› 
.
›› #
DefaultRequestHeaders
›› -
.
››- .
Authorization
››. ;
==
››< >
null
››? C
)
››C D
{
ﬁﬁ 	
var
ﬂﬂ 
token
ﬂﬂ 
=
ﬂﬂ 
await
ﬂﬂ 
_localStorage
ﬂﬂ +
.
ﬂﬂ+ ,
GetItemAsync
ﬂﬂ, 8
<
ﬂﬂ8 9
string
ﬂﬂ9 ?
>
ﬂﬂ? @
(
ﬂﬂ@ A
$str
ﬂﬂA L
)
ﬂﬂL M
;
ﬂﬂM N
_httpClient
‡‡ 
.
‡‡ #
DefaultRequestHeaders
‡‡ -
.
‡‡- .
Authorization
‡‡. ;
=
‡‡< =
new
·· '
AuthenticationHeaderValue
·· 5
(
··5 6
$str
··6 >
,
··> ?
token
··@ E
)
··E F
;
··F G
}
‚‚ 	
return
„„ 
await
„„ 
(
„„ 
(
„„ %
CustomAuthStateProvider
„„ .
)
„„. / 
_authStateProvider
„„/ A
)
„„A B
.
„„B C
isLoggedInAsync
„„C R
(
„„R S
)
„„S T
;
„„T U
}
‰‰ 
public
ÂÂ 

async
ÂÂ 
Task
ÂÂ 
<
ÂÂ 
bool
ÂÂ 
>
ÂÂ 
IsAdminAsync
ÂÂ (
(
ÂÂ( )
)
ÂÂ) *
{
ÊÊ 
var
ÁÁ 
role
ÁÁ 
=
ÁÁ 
(
ÁÁ 
await
ÁÁ 
(
ÁÁ 
(
ÁÁ %
CustomAuthStateProvider
ÁÁ 3
)
ÁÁ3 4 
_authStateProvider
ÁÁ4 F
)
ÁÁF G
.
ÁÁG H)
GetAuthenticationStateAsync
ÁÁH c
(
ÁÁc d
)
ÁÁd e
)
ÁÁe f
.
ËË 
User
ËË 
.
ËË 
Claims
ËË 
.
ËË 
FirstOrDefault
ËË '
(
ËË' (
c
ËË( )
=>
ËË* ,
c
ËË- .
.
ËË. /
Type
ËË/ 3
==
ËË4 6
$str
ËË7 =
)
ËË= >
?
ËË> ?
.
ËË? @
Value
ËË@ E
;
ËËE F
if
ÈÈ 

(
ÈÈ 
role
ÈÈ 
==
ÈÈ 
null
ÈÈ 
)
ÈÈ 
{
ÍÍ 	
return
ÎÎ 
false
ÎÎ 
;
ÎÎ 
}
ÏÏ 	
Console
ÌÌ 
.
ÌÌ 
	WriteLine
ÌÌ 
(
ÌÌ 
$"
ÌÌ 
$str
ÌÌ '
{
ÌÌ' (
role
ÌÌ( ,
}
ÌÌ, -
"
ÌÌ- .
)
ÌÌ. /
;
ÌÌ/ 0
return
ÓÓ 
role
ÓÓ 
.
ÓÓ 
Contains
ÓÓ 
(
ÓÓ 
$str
ÓÓ $
)
ÓÓ$ %
;
ÓÓ% &
}
ÔÔ 
public
ˆˆ 

async
ˆˆ 
Task
ˆˆ 
<
ˆˆ '
AuthenticationHeaderValue
ˆˆ /
?
ˆˆ/ 0
>
ˆˆ0 1!
GetAccessTokenAsync
ˆˆ2 E
(
ˆˆE F
)
ˆˆF G
{
˜˜ 
var
˘˘ 
token
˘˘ 
=
˘˘ 
await
˘˘ 
_localStorage
˘˘ '
.
˘˘' (
GetItemAsync
˘˘( 4
<
˘˘4 5
string
˘˘5 ;
>
˘˘; <
(
˘˘< =
$str
˘˘= H
)
˘˘H I
;
˘˘I J
if
¸¸ 

(
¸¸ 
string
¸¸ 
.
¸¸  
IsNullOrWhiteSpace
¸¸ %
(
¸¸% &
token
¸¸& +
)
¸¸+ ,
)
¸¸, -
{
˝˝ 	
await
˛˛ 
ClearTokensAsync
˛˛ "
(
˛˛" #
)
˛˛# $
;
˛˛$ %
return
ˇˇ 
null
ˇˇ 
;
ˇˇ 
}
ÄÄ 	
var
ÇÇ 
expiry
ÇÇ 
=
ÇÇ &
_jwtSecurityTokenHandler
ÇÇ -
.
ÇÇ- .
ReadJwtToken
ÇÇ. :
(
ÇÇ: ;
token
ÇÇ; @
)
ÇÇ@ A
.
ÇÇA B
Claims
ÇÇB H
.
ÇÇH I
FirstOrDefault
ÇÇI W
(
ÇÇW X
c
ÇÇX Y
=>
ÇÇZ \
c
ÇÇ] ^
.
ÇÇ^ _
Type
ÇÇ_ c
==
ÇÇd f
$str
ÇÇg l
)
ÇÇl m
?
ÇÇm n
.
ÇÇn o
Value
ÇÇo t
;
ÇÇt u
if
ÉÉ 

(
ÉÉ 
expiry
ÉÉ 
==
ÉÉ 
null
ÉÉ 
)
ÉÉ 
{
ÑÑ 	
await
ÖÖ 
ClearTokensAsync
ÖÖ "
(
ÖÖ" #
)
ÖÖ# $
;
ÖÖ$ %
return
ÜÜ 
null
ÜÜ 
;
ÜÜ 
}
áá 	
var
ââ 
expiryDateTime
ââ 
=
ââ 
DateTimeOffset
ââ +
.
ââ+ ,!
FromUnixTimeSeconds
ââ, ?
(
ââ? @
long
ââ@ D
.
ââD E
Parse
ââE J
(
ââJ K
expiry
ââK Q
)
ââQ R
)
ââR S
;
ââS T
if
ãã 

(
ãã 
expiryDateTime
ãã 
<
ãã 
DateTimeOffset
ãã +
.
ãã+ ,
UtcNow
ãã, 2
.
ãã2 3

AddMinutes
ãã3 =
(
ãã= >
$num
ãã> ?
)
ãã? @
)
ãã@ A
{
åå 	
var
éé 
refreshToken
éé 
=
éé 
await
éé $
_localStorage
éé% 2
.
éé2 3
GetItemAsync
éé3 ?
<
éé? @
string
éé@ F
>
ééF G
(
ééG H
$str
ééH V
)
ééV W
;
ééW X
if
èè 
(
èè 
string
èè 
.
èè  
IsNullOrWhiteSpace
èè )
(
èè) *
refreshToken
èè* 6
)
èè6 7
)
èè7 8
{
êê 
await
ëë 
ClearTokensAsync
ëë &
(
ëë& '
)
ëë' (
;
ëë( )
return
íí 
null
íí 
;
íí 
}
ìì 
var
ïï 
_httpClient
ïï 
=
ïï 
_factory
ïï &
.
ïï& '
CreateClient
ïï' 3
(
ïï3 4
$str
ïï4 <
)
ïï< =
;
ïï= >
try
ññ 
{
óó 
var
òò 
request
òò 
=
òò 
new
òò !$
RefreshTokenRequestDto
òò" 8
{
ôô 
AccessToken
öö 
=
öö  !
token
öö" '
,
öö' (
RefreshToken
õõ  
=
õõ! "
refreshToken
õõ# /
}
úú 
;
úú 
var
ùù 
response
ùù 
=
ùù 
await
ùù $
_httpClient
ùù% 0
.
ùù0 1
PostAsJsonAsync
ùù1 @
(
ùù@ A
$str
ùùA Y
,
ùùY Z
request
ùù[ b
)
ùùb c
;
ùùc d
if
üü 
(
üü 
!
üü 
response
üü 
.
üü !
IsSuccessStatusCode
üü 1
)
üü1 2
{
†† 
return
°° 
null
°° 
;
°°  
}
¢¢ 
var
££ 
apiResponse
££ 
=
££  !
await
££" '
response
££( 0
.
££0 1
Content
££1 8
.
££8 9
ReadFromJsonAsync
££9 J
<
££J K
ApiResponse
££K V
<
££V W
AuthResponseDto
££W f
>
££f g
>
££g h
(
££h i
)
££i j
;
££j k
if
•• 
(
•• 
apiResponse
•• 
?
••  
.
••  !
Data
••! %
==
••& (
null
••) -
)
••- .
{
¶¶ 
return
ßß 
null
ßß 
;
ßß  
}
®® 
await
©© 
SetTokensAsync
©© $
(
©©$ %
apiResponse
©©% 0
.
©©0 1
Data
©©1 5
.
©©5 6
AccessToken
©©6 A
,
©©A B
apiResponse
©©C N
.
©©N O
Data
©©O S
.
©©S T
RefreshToken
©©T `
)
©©` a
;
©©a b
return
™™ 
new
™™ '
AuthenticationHeaderValue
™™ 4
(
™™4 5
$str
™™5 =
,
™™= >
apiResponse
™™? J
.
™™J K
Data
™™K O
.
™™O P
AccessToken
™™P [
)
™™[ \
;
™™\ ]
}
´´ 
catch
¨¨ 
{
≠≠ 
await
ÆÆ 
ClearTokensAsync
ÆÆ &
(
ÆÆ& '
)
ÆÆ' (
;
ÆÆ( )
return
ØØ 
null
ØØ 
;
ØØ 
}
∞∞ 
}
±± 	
return
≥≥ 
new
≥≥ '
AuthenticationHeaderValue
≥≥ ,
(
≥≥, -
$str
≥≥- 5
,
≥≥5 6
token
≥≥7 <
)
≥≥< =
;
≥≥= >
}
¥¥ 
private
µµ 
async
µµ 
Task
µµ 
ClearTokensAsync
µµ '
(
µµ' (
)
µµ( )
{
∂∂ 
await
∑∑ 
_localStorage
∑∑ 
.
∑∑ 
RemoveItemAsync
∑∑ +
(
∑∑+ ,
$str
∑∑, 7
)
∑∑7 8
;
∑∑8 9
await
∏∏ 
_localStorage
∏∏ 
.
∏∏ 
RemoveItemAsync
∏∏ +
(
∏∏+ ,
$str
∏∏, :
)
∏∏: ;
;
∏∏; <
}
ππ 
private
∫∫ 
async
∫∫ 
Task
∫∫ 
SetTokensAsync
∫∫ %
(
∫∫% &
string
∫∫& ,
accessToken
∫∫- 8
,
∫∫8 9
string
∫∫: @
refreshToken
∫∫A M
)
∫∫M N
{
ªª 
await
ºº 
_localStorage
ºº 
.
ºº 
SetItemAsync
ºº (
(
ºº( )
$str
ºº) 4
,
ºº4 5
accessToken
ºº6 A
)
ººA B
;
ººB C
await
ΩΩ 
_localStorage
ΩΩ 
.
ΩΩ 
SetItemAsync
ΩΩ (
(
ΩΩ( )
$str
ΩΩ) 7
,
ΩΩ7 8
refreshToken
ΩΩ9 E
)
ΩΩE F
;
ΩΩF G
}
ææ 
}øø ‘Ò
PD:\BootcampProject\default-project-main\src\08.BlazorUI\Services\AdminService.cs
	namespace 	
MyApp
 
. 
BlazorUI 
. 
Services !
;! "
public 
class 
AdminService 
: 
IAdminService )
{ 
private

 
readonly

 
IHttpClientFactory

 '
_factory

( 0
;

0 1
public 

AdminService 
( 
IHttpClientFactory *
factory+ 2
)2 3
{ 
_factory 
= 
factory 
; 
} 
public 

async 
Task 
< 
List 
< 
	CourseDto $
>$ %
>% &
GetAllCourseAsync' 8
(8 9
)9 :
{ 
var 
_httpClient 
= 
_factory "
." #
CreateClient# /
(/ 0
$str0 8
)8 9
;9 :
var 
	parameter 
= 
new !
CourseQueryParameters 1
(1 2
)2 3
;3 4
var 
query 
= 
new 

Dictionary "
<" #
string# )
,) *
string+ 1
?1 2
>2 3
{ 	
[ 
$str 
] 
= 
	parameter "
." #
Search# )
,) *
[ 
$str 
] 
= 
	parameter &
.& '

CategoryId' 1
.1 2
ToString2 :
(: ;
); <
,< =
[ 
$str 
] 
= 
	parameter $
.$ %
MinPrice% -
.- .
ToString. 6
(6 7
)7 8
,8 9
[ 
$str 
] 
= 
	parameter $
.$ %
MaxPrice% -
.- .
ToString. 6
(6 7
)7 8
,8 9
[ 
$str 
] 
= 
	parameter "
." #
SortBy# )
,) *
[ 
$str 
] 
= 
	parameter  )
.) *
SortDirection* 7
,7 8
}   	
;  	 

try!! 
{"" 	
var## 
response## 
=## 
await##  
_httpClient##! ,
.##, -
GetAsync##- 5
(##5 6
QueryHelpers##6 B
.##B C
AddQueryString##C Q
(##Q R
$str##R a
,##a b
query##c h
)##h i
)##i j
;##j k
if$$ 
($$ 
response$$ 
.$$ 
IsSuccessStatusCode$$ ,
)$$, -
{%% 
var&& 
apiResponse&& 
=&&  !
await&&" '
response&&( 0
.&&0 1
Content&&1 8
.&&8 9
ReadFromJsonAsync&&9 J
<&&J K
ApiResponse&&K V
<&&V W
PaginatedResponse&&W h
<&&h i
IEnumerable&&i t
<&&t u
	CourseDto&&u ~
>&&~ 
>	&& Ä
>
&&Ä Å
>
&&Å Ç
(
&&Ç É
)
&&É Ñ
;
&&Ñ Ö
if(( 
((( 
apiResponse(( 
?((  
.((  !
Data((! %
?((% &
.((& '
Data((' +
!=((, .
null((/ 3
)((3 4
{)) 
return** 
apiResponse** &
.**& '
Data**' +
.**+ ,
Data**, 0
.++ 
OrderBy++ $
(++$ %
c++% &
=>++' )
c++* +
.+++ ,
Id++, .
)++. /
.,, 
ToList,, #
(,,# $
),,$ %
;,,% &
}-- 
return.. 
new.. 
(.. 
).. 
;.. 
}// 
return00 
new00 
(00 
)00 
;00 
}11 	
catch22 
(22 
	Exception22 
ex22 
)22 
{33 	
Console44 
.44 
	WriteLine44 
(44 
$"44  
$str44  :
{44: ;
ex44; =
.44= >
Message44> E
}44E F
"44F G
)44G H
;44H I
return55 
new55 
(55 
)55 
;55 
}66 	
}77 
public:: 

async:: 
Task:: 
<:: 
	CourseDto:: 
?::  
>::  !
CreateCourseAsync::" 3
(::3 4%
AuthenticationHeaderValue::4 M
authorization::N [
,::[ \
CreateCourseDto::] l
request::m t
)::t u
{;; 
var<< 
_httpClient<< 
=<< 
_factory<< "
.<<" #
CreateClient<<# /
(<</ 0
$str<<0 8
)<<8 9
;<<9 :
_httpClient== 
.== !
DefaultRequestHeaders== )
.==) *
Authorization==* 7
===8 9
authorization==: G
;==G H
try>> 
{?? 	
var@@ 
response@@ 
=@@ 
await@@  
_httpClient@@! ,
.@@, -
PostAsJsonAsync@@- <
(@@< =
$str@@= I
,@@I J
request@@K R
)@@R S
;@@S T
ifAA 
(AA 
responseAA 
.AA 
IsSuccessStatusCodeAA ,
)AA, -
{BB 
varCC 
apiResponseCC 
=CC  !
awaitCC" '
responseCC( 0
.CC0 1
ContentCC1 8
.CC8 9
ReadFromJsonAsyncCC9 J
<CCJ K
ApiResponseCCK V
<CCV W
	CourseDtoCCW `
>CC` a
>CCa b
(CCb c
)CCc d
;CCd e
ifEE 
(EE 
apiResponseEE 
?EE  
.EE  !
DataEE! %
!=EE& (
nullEE) -
)EE- .
{FF 
returnGG 
apiResponseGG &
.GG& '
DataGG' +
;GG+ ,
}HH 
returnII 
nullII 
;II 
}JJ 
returnKK 
nullKK 
;KK 
}LL 	
catchMM 
(MM 
	ExceptionMM 
exMM 
)MM 
{NN 	
ConsoleOO 
.OO 
	WriteLineOO 
(OO 
$"OO  
$strOO  :
{OO: ;
exOO; =
.OO= >
MessageOO> E
}OOE F
"OOF G
)OOG H
;OOH I
returnPP 
nullPP 
;PP 
}QQ 	
}RR 
publicTT 

asyncTT 
TaskTT 
<TT 
	CourseDtoTT 
?TT  
>TT  !
UpdateCourseAsyncTT" 3
(TT3 4%
AuthenticationHeaderValueTT4 M
authorizationTTN [
,TT[ \
intTT] `
idTTa c
,TTc d
UpdateCourseDtoTTe t
requestTTu |
)TT| }
{UU 
varVV 
_httpClientVV 
=VV 
_factoryVV "
.VV" #
CreateClientVV# /
(VV/ 0
$strVV0 8
)VV8 9
;VV9 :
_httpClientWW 
.WW !
DefaultRequestHeadersWW )
.WW) *
AuthorizationWW* 7
=WW8 9
authorizationWW: G
;WWG H
tryYY 
{ZZ 	
var[[ 
response[[ 
=[[ 
await[[  
_httpClient[[! ,
.[[, -
PutAsJsonAsync[[- ;
([[; <
$"[[< >
$str[[> I
{[[I J
id[[J L
}[[L M
"[[M N
,[[N O
request[[P W
)[[W X
;[[X Y
if\\ 
(\\ 
response\\ 
.\\ 
IsSuccessStatusCode\\ ,
)\\, -
{]] 
var^^ 
apiResponse^^ 
=^^  !
await^^" '
response^^( 0
.^^0 1
Content^^1 8
.^^8 9
ReadFromJsonAsync^^9 J
<^^J K
ApiResponse^^K V
<^^V W
	CourseDto^^W `
>^^` a
>^^a b
(^^b c
)^^c d
;^^d e
if`` 
(`` 
apiResponse`` 
?``  
.``  !
Data``! %
!=``& (
null``) -
)``- .
{aa 
returnbb 
apiResponsebb &
.bb& '
Databb' +
;bb+ ,
}cc 
returndd 
nulldd 
;dd 
}ee 
returnff 
nullff 
;ff 
}gg 	
catchhh 
(hh 
	Exceptionhh 
exhh 
)hh 
{ii 	
Consolejj 
.jj 
	WriteLinejj 
(jj 
$"jj  
$strjj  :
{jj: ;
exjj; =
.jj= >
Messagejj> E
}jjE F
"jjF G
)jjG H
;jjH I
returnkk 
nullkk 
;kk 
}ll 	
}mm 
publicoo 

asyncoo 
Taskoo 
<oo 
booloo 
>oo 
DeleteCourseAsyncoo -
(oo- .%
AuthenticationHeaderValueoo. G
authorizationooH U
,ooU V
intooW Z
idoo[ ]
)oo] ^
{pp 
varqq 
_httpClientqq 
=qq 
_factoryqq "
.qq" #
CreateClientqq# /
(qq/ 0
$strqq0 8
)qq8 9
;qq9 :
_httpClientrr 
.rr !
DefaultRequestHeadersrr )
.rr) *
Authorizationrr* 7
=rr8 9
authorizationrr: G
;rrG H
trytt 
{uu 	
varvv 
responsevv 
=vv 
awaitvv  
_httpClientvv! ,
.vv, -
DeleteAsyncvv- 8
(vv8 9
$"vv9 ;
$strvv; F
{vvF G
idvvG I
}vvI J
"vvJ K
)vvK L
;vvL M
ifww 
(ww 
!ww 
responseww 
.ww 
IsSuccessStatusCodeww -
)ww- .
{xx 
returnyy 
falseyy 
;yy 
}zz 
var{{ 
apiResponse{{ 
={{ 
await{{ #
response{{$ ,
.{{, -
Content{{- 4
.{{4 5
ReadFromJsonAsync{{5 F
<{{F G
ApiResponse{{G R
<{{R S
object{{S Y
>{{Y Z
>{{Z [
({{[ \
){{\ ]
;{{] ^
return|| 
apiResponse|| 
!=|| !
null||" &
;||& '
}}} 	
catch~~ 
(~~ 
	Exception~~ 
ex~~ 
)~~ 
{ 	
Console
ÄÄ 
.
ÄÄ 
	WriteLine
ÄÄ 
(
ÄÄ 
$"
ÄÄ  
$str
ÄÄ  :
{
ÄÄ: ;
ex
ÄÄ; =
.
ÄÄ= >
Message
ÄÄ> E
}
ÄÄE F
"
ÄÄF G
)
ÄÄG H
;
ÄÄH I
return
ÅÅ 
false
ÅÅ 
;
ÅÅ 
}
ÇÇ 	
}
ÉÉ 
public
ÖÖ 

async
ÖÖ 
Task
ÖÖ 
<
ÖÖ 
List
ÖÖ 
<
ÖÖ 
CategoryDto
ÖÖ &
>
ÖÖ& '
>
ÖÖ' (!
GetAllCategoryAsync
ÖÖ) <
(
ÖÖ< =
)
ÖÖ= >
{
ÜÜ 
var
áá 
_httpClient
áá 
=
áá 
_factory
áá "
.
áá" #
CreateClient
áá# /
(
áá/ 0
$str
áá0 8
)
áá8 9
;
áá9 :
try
àà 
{
ââ 	
var
ää 
response
ää 
=
ää 
await
ää  
_httpClient
ää! ,
.
ää, -
GetAsync
ää- 5
(
ää5 6
$str
ää6 D
)
ääD E
;
ääE F
if
ãã 
(
ãã 
response
ãã 
.
ãã !
IsSuccessStatusCode
ãã ,
)
ãã, -
{
åå 
var
çç 
apiResponse
çç 
=
çç  !
await
çç" '
response
çç( 0
.
çç0 1
Content
çç1 8
.
çç8 9
ReadFromJsonAsync
çç9 J
<
ççJ K
ApiResponse
ççK V
<
ççV W
List
ççW [
<
çç[ \
CategoryDto
çç\ g
>
ççg h
>
ççh i
>
ççi j
(
ççj k
)
ççk l
;
ççl m
if
èè 
(
èè 
apiResponse
èè 
?
èè  
.
èè  !
Data
èè! %
!=
èè& (
null
èè) -
)
èè- .
{
êê 
return
ëë 
apiResponse
ëë &
.
ëë& '
Data
ëë' +
;
ëë+ ,
}
íí 
return
ìì 
new
ìì 
(
ìì 
)
ìì 
;
ìì 
}
îî 
return
ïï 
new
ïï 
(
ïï 
)
ïï 
;
ïï 
}
ññ 	
catch
óó 
(
óó 
	Exception
óó 
ex
óó 
)
óó 
{
òò 	
Console
ôô 
.
ôô 
	WriteLine
ôô 
(
ôô 
$"
ôô  
$str
ôô  B
{
ôôB C
ex
ôôC E
.
ôôE F
Message
ôôF M
}
ôôM N
"
ôôN O
)
ôôO P
;
ôôP Q
return
öö 
new
öö 
(
öö 
)
öö 
;
öö 
}
õõ 	
}
úú 
public
üü 

async
üü 
Task
üü 
<
üü 
CategoryDto
üü !
?
üü! "
>
üü" #!
CreateCategoryAsync
üü$ 7
(
üü7 8'
AuthenticationHeaderValue
üü8 Q
authorization
üüR _
,
üü_ `
CreateCategoryDto
üüa r
request
üüs z
)
üüz {
{
†† 
var
°° 
_httpClient
°° 
=
°° 
_factory
°° "
.
°°" #
CreateClient
°°# /
(
°°/ 0
$str
°°0 8
)
°°8 9
;
°°9 :
_httpClient
¢¢ 
.
¢¢ #
DefaultRequestHeaders
¢¢ )
.
¢¢) *
Authorization
¢¢* 7
=
¢¢8 9
authorization
¢¢: G
;
¢¢G H
try
££ 
{
§§ 	
var
•• 
response
•• 
=
•• 
await
••  
_httpClient
••! ,
.
••, -
PostAsJsonAsync
••- <
(
••< =
$str
••= K
,
••K L
request
••M T
)
••T U
;
••U V
if
¶¶ 
(
¶¶ 
response
¶¶ 
.
¶¶ !
IsSuccessStatusCode
¶¶ ,
)
¶¶, -
{
ßß 
var
®® 
apiResponse
®® 
=
®®  !
await
®®" '
response
®®( 0
.
®®0 1
Content
®®1 8
.
®®8 9
ReadFromJsonAsync
®®9 J
<
®®J K
ApiResponse
®®K V
<
®®V W
CategoryDto
®®W b
>
®®b c
>
®®c d
(
®®d e
)
®®e f
;
®®f g
if
™™ 
(
™™ 
apiResponse
™™ 
?
™™  
.
™™  !
Data
™™! %
!=
™™& (
null
™™) -
)
™™- .
{
´´ 
return
¨¨ 
apiResponse
¨¨ &
.
¨¨& '
Data
¨¨' +
;
¨¨+ ,
}
≠≠ 
return
ÆÆ 
null
ÆÆ 
;
ÆÆ 
}
ØØ 
return
∞∞ 
null
∞∞ 
;
∞∞ 
}
±± 	
catch
≤≤ 
(
≤≤ 
	Exception
≤≤ 
ex
≤≤ 
)
≤≤ 
{
≥≥ 	
Console
¥¥ 
.
¥¥ 
	WriteLine
¥¥ 
(
¥¥ 
$"
¥¥  
$str
¥¥  :
{
¥¥: ;
ex
¥¥; =
.
¥¥= >
Message
¥¥> E
}
¥¥E F
"
¥¥F G
)
¥¥G H
;
¥¥H I
return
µµ 
null
µµ 
;
µµ 
}
∂∂ 	
}
∑∑ 
public
ππ 

async
ππ 
Task
ππ 
<
ππ 
CategoryDto
ππ !
?
ππ! "
>
ππ" #!
UpdateCategoryAsync
ππ$ 7
(
ππ7 8'
AuthenticationHeaderValue
ππ8 Q
authorization
ππR _
,
ππ_ `
int
ππa d
id
ππe g
,
ππg h
UpdateCategoryDto
ππi z
requestππ{ Ç
)ππÇ É
{
∫∫ 
var
ªª 
_httpClient
ªª 
=
ªª 
_factory
ªª "
.
ªª" #
CreateClient
ªª# /
(
ªª/ 0
$str
ªª0 8
)
ªª8 9
;
ªª9 :
_httpClient
ºº 
.
ºº #
DefaultRequestHeaders
ºº )
.
ºº) *
Authorization
ºº* 7
=
ºº8 9
authorization
ºº: G
;
ººG H
try
ææ 
{
øø 	
var
¿¿ 
response
¿¿ 
=
¿¿ 
await
¿¿  
_httpClient
¿¿! ,
.
¿¿, -
PutAsJsonAsync
¿¿- ;
(
¿¿; <
$"
¿¿< >
$str
¿¿> K
{
¿¿K L
id
¿¿L N
}
¿¿N O
"
¿¿O P
,
¿¿P Q
request
¿¿R Y
)
¿¿Y Z
;
¿¿Z [
if
¡¡ 
(
¡¡ 
response
¡¡ 
.
¡¡ !
IsSuccessStatusCode
¡¡ ,
)
¡¡, -
{
¬¬ 
var
√√ 
apiResponse
√√ 
=
√√  !
await
√√" '
response
√√( 0
.
√√0 1
Content
√√1 8
.
√√8 9
ReadFromJsonAsync
√√9 J
<
√√J K
ApiResponse
√√K V
<
√√V W
CategoryDto
√√W b
>
√√b c
>
√√c d
(
√√d e
)
√√e f
;
√√f g
if
≈≈ 
(
≈≈ 
apiResponse
≈≈ 
?
≈≈  
.
≈≈  !
Data
≈≈! %
!=
≈≈& (
null
≈≈) -
)
≈≈- .
{
∆∆ 
return
«« 
apiResponse
«« &
.
««& '
Data
««' +
;
««+ ,
}
»» 
return
…… 
null
…… 
;
…… 
}
   
return
ÀÀ 
null
ÀÀ 
;
ÀÀ 
}
ÃÃ 	
catch
ÕÕ 
(
ÕÕ 
	Exception
ÕÕ 
ex
ÕÕ 
)
ÕÕ 
{
ŒŒ 	
Console
œœ 
.
œœ 
	WriteLine
œœ 
(
œœ 
$"
œœ  
$str
œœ  :
{
œœ: ;
ex
œœ; =
.
œœ= >
Message
œœ> E
}
œœE F
"
œœF G
)
œœG H
;
œœH I
return
–– 
null
–– 
;
–– 
}
—— 	
}
““ 
public
‘‘ 

async
‘‘ 
Task
‘‘ 
<
‘‘ 
bool
‘‘ 
>
‘‘ !
DeleteCategoryAsync
‘‘ /
(
‘‘/ 0'
AuthenticationHeaderValue
‘‘0 I
authorization
‘‘J W
,
‘‘W X
int
‘‘Y \
id
‘‘] _
)
‘‘_ `
{
’’ 
var
÷÷ 
_httpClient
÷÷ 
=
÷÷ 
_factory
÷÷ "
.
÷÷" #
CreateClient
÷÷# /
(
÷÷/ 0
$str
÷÷0 8
)
÷÷8 9
;
÷÷9 :
_httpClient
◊◊ 
.
◊◊ #
DefaultRequestHeaders
◊◊ )
.
◊◊) *
Authorization
◊◊* 7
=
◊◊8 9
authorization
◊◊: G
;
◊◊G H
try
ŸŸ 
{
⁄⁄ 	
var
€€ 
response
€€ 
=
€€ 
await
€€  
_httpClient
€€! ,
.
€€, -
DeleteAsync
€€- 8
(
€€8 9
$"
€€9 ;
$str
€€; H
{
€€H I
id
€€I K
}
€€K L
"
€€L M
)
€€M N
;
€€N O
if
‹‹ 
(
‹‹ 
!
‹‹ 
response
‹‹ 
.
‹‹ !
IsSuccessStatusCode
‹‹ -
)
‹‹- .
{
›› 
return
ﬁﬁ 
false
ﬁﬁ 
;
ﬁﬁ 
}
ﬂﬂ 
var
‡‡ 
apiResponse
‡‡ 
=
‡‡ 
await
‡‡ #
response
‡‡$ ,
.
‡‡, -
Content
‡‡- 4
.
‡‡4 5
ReadFromJsonAsync
‡‡5 F
<
‡‡F G
ApiResponse
‡‡G R
<
‡‡R S
object
‡‡S Y
>
‡‡Y Z
>
‡‡Z [
(
‡‡[ \
)
‡‡\ ]
;
‡‡] ^
return
·· 
apiResponse
·· 
!=
·· !
null
··" &
;
··& '
}
‚‚ 	
catch
„„ 
(
„„ 
	Exception
„„ 
ex
„„ 
)
„„ 
{
‰‰ 	
Console
ÂÂ 
.
ÂÂ 
	WriteLine
ÂÂ 
(
ÂÂ 
$"
ÂÂ  
$str
ÂÂ  :
{
ÂÂ: ;
ex
ÂÂ; =
.
ÂÂ= >
Message
ÂÂ> E
}
ÂÂE F
"
ÂÂF G
)
ÂÂG H
;
ÂÂH I
return
ÊÊ 
false
ÊÊ 
;
ÊÊ 
}
ÁÁ 	
}
ËË 
public
ÍÍ 

async
ÍÍ 
Task
ÍÍ 
<
ÍÍ 
List
ÍÍ 
<
ÍÍ 

PaymentDto
ÍÍ %
>
ÍÍ% &
>
ÍÍ& ''
GetAllPaymentMethodsAsync
ÍÍ( A
(
ÍÍA B
)
ÍÍB C
{
ÎÎ 
var
ÏÏ 
_httpClient
ÏÏ 
=
ÏÏ 
_factory
ÏÏ "
.
ÏÏ" #
CreateClient
ÏÏ# /
(
ÏÏ/ 0
$str
ÏÏ0 8
)
ÏÏ8 9
;
ÏÏ9 :
try
ÌÌ 
{
ÓÓ 	
var
ÔÔ 
response
ÔÔ 
=
ÔÔ 
await
ÔÔ  
_httpClient
ÔÔ! ,
.
ÔÔ, -
GetAsync
ÔÔ- 5
(
ÔÔ5 6
$str
ÔÔ6 C
)
ÔÔC D
;
ÔÔD E
if
 
(
 
response
 
.
 !
IsSuccessStatusCode
 ,
)
, -
{
ÒÒ 
var
ÚÚ 
apiResponse
ÚÚ 
=
ÚÚ  !
await
ÚÚ" '
response
ÚÚ( 0
.
ÚÚ0 1
Content
ÚÚ1 8
.
ÚÚ8 9
ReadFromJsonAsync
ÚÚ9 J
<
ÚÚJ K
ApiResponse
ÚÚK V
<
ÚÚV W
List
ÚÚW [
<
ÚÚ[ \

PaymentDto
ÚÚ\ f
>
ÚÚf g
>
ÚÚg h
>
ÚÚh i
(
ÚÚi j
)
ÚÚj k
;
ÚÚk l
if
ÙÙ 
(
ÙÙ 
apiResponse
ÙÙ 
?
ÙÙ  
.
ÙÙ  !
Data
ÙÙ! %
!=
ÙÙ& (
null
ÙÙ) -
)
ÙÙ- .
{
ıı 
return
ˆˆ 
apiResponse
ˆˆ &
.
ˆˆ& '
Data
ˆˆ' +
;
ˆˆ+ ,
}
˜˜ 
return
¯¯ 
new
¯¯ 
(
¯¯ 
)
¯¯ 
;
¯¯ 
}
˘˘ 
return
˙˙ 
new
˙˙ 
(
˙˙ 
)
˙˙ 
;
˙˙ 
}
˚˚ 	
catch
¸¸ 
(
¸¸ 
	Exception
¸¸ 
ex
¸¸ 
)
¸¸ 
{
˝˝ 	
Console
˛˛ 
.
˛˛ 
	WriteLine
˛˛ 
(
˛˛ 
$"
˛˛  
$str
˛˛  B
{
˛˛B C
ex
˛˛C E
.
˛˛E F
Message
˛˛F M
}
˛˛M N
"
˛˛N O
)
˛˛O P
;
˛˛P Q
return
ˇˇ 
new
ˇˇ 
(
ˇˇ 
)
ˇˇ 
;
ˇˇ 
}
ÄÄ 	
}
ÅÅ 
public
ÉÉ 

async
ÉÉ 
Task
ÉÉ 
<
ÉÉ 

PaymentDto
ÉÉ  
?
ÉÉ  !
>
ÉÉ! "&
CreatePaymentMethodAsync
ÉÉ# ;
(
ÉÉ; <'
AuthenticationHeaderValue
ÉÉ< U
authorization
ÉÉV c
,
ÉÉc d
CreatePaymentDto
ÉÉe u
request
ÉÉv }
)
ÉÉ} ~
{
ÑÑ 
var
ÖÖ 
_httpClient
ÖÖ 
=
ÖÖ 
_factory
ÖÖ "
.
ÖÖ" #
CreateClient
ÖÖ# /
(
ÖÖ/ 0
$str
ÖÖ0 8
)
ÖÖ8 9
;
ÖÖ9 :
_httpClient
ÜÜ 
.
ÜÜ #
DefaultRequestHeaders
ÜÜ )
.
ÜÜ) *
Authorization
ÜÜ* 7
=
ÜÜ8 9
authorization
ÜÜ: G
;
ÜÜG H
try
áá 
{
àà 	
var
ââ 
response
ââ 
=
ââ 
await
ââ  
_httpClient
ââ! ,
.
ââ, -
PostAsJsonAsync
ââ- <
(
ââ< =
$str
ââ= J
,
ââJ K
request
ââL S
)
ââS T
;
ââT U
if
ää 
(
ää 
response
ää 
.
ää !
IsSuccessStatusCode
ää ,
)
ää, -
{
ãã 
var
åå 
apiResponse
åå 
=
åå  !
await
åå" '
response
åå( 0
.
åå0 1
Content
åå1 8
.
åå8 9
ReadFromJsonAsync
åå9 J
<
ååJ K
ApiResponse
ååK V
<
ååV W

PaymentDto
ååW a
>
ååa b
>
ååb c
(
ååc d
)
ååd e
;
ååe f
if
éé 
(
éé 
apiResponse
éé 
?
éé  
.
éé  !
Data
éé! %
!=
éé& (
null
éé) -
)
éé- .
{
èè 
return
êê 
apiResponse
êê &
.
êê& '
Data
êê' +
;
êê+ ,
}
ëë 
return
íí 
null
íí 
;
íí 
}
ìì 
return
îî 
null
îî 
;
îî 
}
ïï 	
catch
ññ 
(
ññ 
	Exception
ññ 
ex
ññ 
)
ññ 
{
óó 	
Console
òò 
.
òò 
	WriteLine
òò 
(
òò 
$"
òò  
$str
òò  A
{
òòA B
ex
òòB D
.
òòD E
Message
òòE L
}
òòL M
"
òòM N
)
òòN O
;
òòO P
return
ôô 
null
ôô 
;
ôô 
}
öö 	
}
õõ 
public
ùù 

async
ùù 
Task
ùù 
<
ùù 

PaymentDto
ùù  
?
ùù  !
>
ùù! "&
UpdatePaymentMethodAsync
ùù# ;
(
ùù; <'
AuthenticationHeaderValue
ùù< U
authorization
ùùV c
,
ùùc d
int
ùùe h
id
ùùi k
,
ùùk l
UpdatePaymentDto
ùùm }
requestùù~ Ö
)ùùÖ Ü
{
ûû 
var
üü 
_httpClient
üü 
=
üü 
_factory
üü "
.
üü" #
CreateClient
üü# /
(
üü/ 0
$str
üü0 8
)
üü8 9
;
üü9 :
_httpClient
†† 
.
†† #
DefaultRequestHeaders
†† )
.
††) *
Authorization
††* 7
=
††8 9
authorization
††: G
;
††G H
try
¢¢ 
{
££ 	
var
§§ 
response
§§ 
=
§§ 
await
§§  
_httpClient
§§! ,
.
§§, -
PutAsJsonAsync
§§- ;
(
§§; <
$"
§§< >
$str
§§> J
{
§§J K
id
§§K M
}
§§M N
"
§§N O
,
§§O P
request
§§Q X
)
§§X Y
;
§§Y Z
if
¶¶ 
(
¶¶ 
response
¶¶ 
.
¶¶ !
IsSuccessStatusCode
¶¶ ,
)
¶¶, -
{
ßß 
var
®® 
apiResponse
®® 
=
®®  !
await
®®" '
response
®®( 0
.
®®0 1
Content
®®1 8
.
®®8 9
ReadFromJsonAsync
®®9 J
<
®®J K
ApiResponse
®®K V
<
®®V W

PaymentDto
®®W a
>
®®a b
>
®®b c
(
®®c d
)
®®d e
;
®®e f
if
™™ 
(
™™ 
apiResponse
™™ 
?
™™  
.
™™  !
Data
™™! %
!=
™™& (
null
™™) -
)
™™- .
{
´´ 
return
¨¨ 
apiResponse
¨¨ &
.
¨¨& '
Data
¨¨' +
;
¨¨+ ,
}
≠≠ 
return
ÆÆ 
null
ÆÆ 
;
ÆÆ 
}
ØØ 
return
∞∞ 
null
∞∞ 
;
∞∞ 
}
±± 	
catch
≤≤ 
(
≤≤ 
	Exception
≤≤ 
ex
≤≤ 
)
≤≤ 
{
≥≥ 	
Console
¥¥ 
.
¥¥ 
	WriteLine
¥¥ 
(
¥¥ 
$"
¥¥  
$str
¥¥  A
{
¥¥A B
ex
¥¥B D
.
¥¥D E
Message
¥¥E L
}
¥¥L M
"
¥¥M N
)
¥¥N O
;
¥¥O P
return
µµ 
null
µµ 
;
µµ 
}
∂∂ 	
}
∑∑ 
public
ππ 

async
ππ 
Task
ππ 
<
ππ 
bool
ππ 
>
ππ &
DeletePaymentMethodAsync
ππ 4
(
ππ4 5'
AuthenticationHeaderValue
ππ5 N
authorization
ππO \
,
ππ\ ]
int
ππ^ a
id
ππb d
)
ππd e
{
∫∫ 
var
ªª 
_httpClient
ªª 
=
ªª 
_factory
ªª "
.
ªª" #
CreateClient
ªª# /
(
ªª/ 0
$str
ªª0 8
)
ªª8 9
;
ªª9 :
_httpClient
ºº 
.
ºº #
DefaultRequestHeaders
ºº )
.
ºº) *
Authorization
ºº* 7
=
ºº8 9
authorization
ºº: G
;
ººG H
try
ææ 
{
øø 	
var
¿¿ 
response
¿¿ 
=
¿¿ 
await
¿¿  
_httpClient
¿¿! ,
.
¿¿, -
DeleteAsync
¿¿- 8
(
¿¿8 9
$"
¿¿9 ;
$str
¿¿; G
{
¿¿G H
id
¿¿H J
}
¿¿J K
"
¿¿K L
)
¿¿L M
;
¿¿M N
if
¡¡ 
(
¡¡ 
!
¡¡ 
response
¡¡ 
.
¡¡ !
IsSuccessStatusCode
¡¡ -
)
¡¡- .
{
¬¬ 
return
√√ 
false
√√ 
;
√√ 
}
ƒƒ 
var
≈≈ 
apiResponse
≈≈ 
=
≈≈ 
await
≈≈ #
response
≈≈$ ,
.
≈≈, -
Content
≈≈- 4
.
≈≈4 5
ReadFromJsonAsync
≈≈5 F
<
≈≈F G
ApiResponse
≈≈G R
<
≈≈R S
object
≈≈S Y
>
≈≈Y Z
>
≈≈Z [
(
≈≈[ \
)
≈≈\ ]
;
≈≈] ^
return
∆∆ 
apiResponse
∆∆ 
!=
∆∆ !
null
∆∆" &
;
∆∆& '
}
«« 	
catch
»» 
(
»» 
	Exception
»» 
ex
»» 
)
»» 
{
…… 	
Console
   
.
   
	WriteLine
   
(
   
$"
    
$str
    A
{
  A B
ex
  B D
.
  D E
Message
  E L
}
  L M
"
  M N
)
  N O
;
  O P
return
ÀÀ 
false
ÀÀ 
;
ÀÀ 
}
ÃÃ 	
}
ÕÕ 
}ŒŒ ù%
BD:\BootcampProject\default-project-main\src\08.BlazorUI\Program.cs
var

 
builder

 
=

 
WebApplication

 
.

 
CreateBuilder

 *
(

* +
args

+ /
)

/ 0
;

0 1
builder 
. 
Services 
. 
AddMudServices 
(  
)  !
;! "
builder 
. 
Services 
. 
AddRazorComponents #
(# $
)$ %
. *
AddInteractiveServerComponents #
(# $
)$ %
;% &
builder 
. 
Services 
. #
AddBlazoredLocalStorage (
(( )
)) *
;* +
builder 
. 
Services 
. 
AddHttpClient 
( 
$str '
,' (
sp) +
=>, .
{ 
sp 
. 
BaseAddress 
= 
new 
Uri 
( 
builder $
.$ %
Configuration% 2
[2 3
$str3 ?
]? @
??A C
$strD \
)\ ]
;] ^
} 
) 
; 
builder 
. 
Services 
. 
	AddScoped 
<  
NavigationManagerExt /
>/ 0
(0 1
)1 2
;2 3
builder!! 
.!! 
Services!! 
.!! 
	AddScoped!! 
<!! 
IUserService!! '
,!!' (
UserService!!) 4
>!!4 5
(!!5 6
)!!6 7
;!!7 8
builder"" 
."" 
Services"" 
."" 
	AddScoped"" 
<"" 
ICheckoutService"" +
,""+ ,
CheckoutService""- <
>""< =
(""= >
)""> ?
;""? @
builder## 
.## 
Services## 
.## 
	AddScoped## 
<## 
IAdminService## (
,##( )
AdminService##* 6
>##6 7
(##7 8
)##8 9
;##9 :
builder&& 
.&& 
Services&& 
.&& 
	AddScoped&& 
<&& '
AuthenticationStateProvider&& 6
,&&6 7#
CustomAuthStateProvider&&8 O
>&&O P
(&&P Q
)&&Q R
;&&R S
builder'' 
.'' 
Services'' 
.'' 
	AddScoped'' 
<'' 
IAuthService'' '
,''' (
AuthService'') 4
>''4 5
(''5 6
)''6 7
;''7 8
builder(( 
.(( 
Services(( 
.(( 
	AddScoped(( 
<(( 
IMyClassService(( *
,((* +
MyClassService((, :
>((: ;
(((; <
)((< =
;((= >
builder** 
.** 
Services** 
.** 
	AddScoped** 
<** 
ICourseService** )
,**) *
CourseService**+ 8
>**8 9
(**9 :
)**: ;
;**; <
builder-- 
.-- 
Services-- 
.-- 
	AddScoped-- 
<-- 
ErrorService-- '
,--' (
ErrorService--) 5
>--5 6
(--6 7
)--7 8
;--8 9
var// 
app// 
=// 	
builder//
 
.// 
Build// 
(// 
)// 
;// 
if22 
(22 
!22 
app22 
.22 	
Environment22	 
.22 
IsDevelopment22 "
(22" #
)22# $
)22$ %
{33 
app44 
.44 
UseExceptionHandler44 
(44 
$str44 $
,44$ % 
createScopeForErrors44& :
:44: ;
true44< @
)44@ A
;44A B
app66 
.66 
UseHsts66 
(66 
)66 
;66 
}77 
app99 
.99 
UseHttpsRedirection99 
(99 
)99 
;99 
app:: 
.:: 
UseAntiforgery:: 
(:: 
):: 
;:: 
app<< 
.<< 
MapStaticAssets<< 
(<< 
)<< 
;<< 
app== 
.== 
MapRazorComponents== 
<== 
App== 
>== 
(== 
)== 
.>> *
AddInteractiveServerRenderMode>> #
(>># $
)>>$ %
;>>% &
app@@ 
.@@ 
Run@@ 
(@@ 
)@@ 	
;@@	 
∫8
`D:\BootcampProject\default-project-main\src\08.BlazorUI\Components\Pages\NavigationManagerExt.cs
	namespace 	
MyApp
 
. 
BlazorUI 
. 

Components #
;# $
public 
class  
NavigationManagerExt !
{ 
private 
readonly 
NavigationManager &
_navigation' 2
;2 3
public		 
 
NavigationManagerExt		 
(		  
NavigationManager		  1

navigation		2 <
)		< =
{

 
_navigation 
= 

navigation  
;  !
} 
public 

void 
	GoToLogin 
( 
) 
{ 
_navigation 
. 

NavigateTo 
( 
$str '
)' (
;( )
} 
public 

void 
GoToRegister 
( 
) 
{ 
_navigation 
. 

NavigateTo 
( 
$str *
)* +
;+ ,
} 
public 

void 
GoToProfile 
( 
) 
{ 
_navigation 
. 

NavigateTo 
( 
$str )
)) *
;* +
} 
public 

void 
GoToEditProfile 
(  
)  !
{ 
_navigation 
. 

NavigateTo 
( 
$str .
). /
;/ 0
}   
public"" 

void"" 
GoToHome"" 
("" 
)"" 
{## 
_navigation$$ 
.$$ 

NavigateTo$$ 
($$ 
$str$$ "
)$$" #
;$$# $
}%% 
public** 

void** 
	GoToHome2** 
(** 
)** 
{++ 
_navigation,, 
.,, 

NavigateTo,, 
(,, 
$str,, "
,,," #
true,,$ (
),,( )
;,,) *
}-- 
public// 

void// 
GoToForgotPass// 
(// 
)//  
{00 
_navigation11 
.11 

NavigateTo11 
(11 
$str11 1
)111 2
;112 3
}22 
public44 

void44 
GoToNewPass44 
(44 
)44 
{55 
_navigation66 
.66 

NavigateTo66 
(66 
$str66 0
)660 1
;661 2
}77 
public99 

void99 #
GoToEmailConfirmSuccess99 '
(99' (
)99( )
{:: 
_navigation;; 
.;; 

NavigateTo;; 
(;; 
$str;; /
);;/ 0
;;;0 1
}<< 
public>> 

void>> 
GoToSuccessPurchase>> #
(>># $
)>>$ %
{?? 
_navigation@@ 
.@@ 

NavigateTo@@ 
(@@ 
$str@@ 2
)@@2 3
;@@3 4
}AA 
publicCC 

voidCC 
GoToKelasKuCC 
(CC 
)CC 
{DD 
_navigationEE 
.EE 

NavigateToEE 
(EE 
$strEE )
)EE) *
;EE* +
}FF 
publicHH 

voidHH 
GoToCheckoutHH 
(HH 
)HH 
{II 
_navigationJJ 
.JJ 

NavigateToJJ 
(JJ 
$strJJ *
)JJ* +
;JJ+ ,
}KK 
publicMM 

voidMM 
GoToInvoiceMM 
(MM 
)MM 
{NN 
_navigationOO 
.OO 

NavigateToOO 
(OO 
$strOO )
)OO) *
;OO* +
}PP 
publicRR 

voidRR 
GoToDetailsInvoiceRR "
(RR" #
intRR# &
	invoiceIdRR' 0
)RR0 1
{SS 
_navigationTT 
.TT 

NavigateToTT 
(TT 
$"TT !
$strTT! 2
{TT2 3
	invoiceIdTT3 <
}TT< =
"TT= >
)TT> ?
;TT? @
}UU 
publicWW 

voidWW #
GoToDetailsInvoiceAdminWW '
(WW' (
intWW( +
	invoiceIdWW, 5
)WW5 6
{XX 
_navigationYY 
.YY 

NavigateToYY 
(YY 
$"YY !
$strYY! 8
{YY8 9
	invoiceIdYY9 B
}YYB C
"YYC D
)YYD E
;YYE F
}ZZ 
public\\ 

void\\ 
GoToDashboard\\ 
(\\ 
)\\ 
{]] 
_navigation^^ 
.^^ 

NavigateTo^^ 
(^^ 
$str^^ +
)^^+ ,
;^^, -
}__ 
publicaa 

voidaa 
GoToUserManagementaa "
(aa" #
)aa# $
{bb 
_navigationcc 
.cc 

NavigateTocc 
(cc 
$strcc 1
)cc1 2
;cc2 3
}dd 
publicff 

voidff "
GoToCategoryManagementff &
(ff& '
)ff' (
{gg 
_navigationhh 
.hh 

NavigateTohh 
(hh 
$strhh *
)hh* +
;hh+ ,
}ii 
publickk 

voidkk  
GoToCourseManagementkk $
(kk$ %
)kk% &
{ll 
_navigationmm 
.mm 

NavigateTomm 
(mm 
$strmm (
)mm( )
;mm) *
}nn 
publicpp 

voidpp 
GoToPaymentMethodpp !
(pp! "
)pp" #
{qq 
_navigationrr 
.rr 

NavigateTorr 
(rr 
$strrr 0
)rr0 1
;rr1 2
}ss 
publicuu 

voiduu %
GoToPaymentMethodSelecteduu )
(uu) *
stringuu* 0
selectedPaymentuu1 @
)uu@ A
{vv 
_navigationww 
.ww 

NavigateToww 
(ww 
$"ww !
$strww! :
{ww: ;
selectedPaymentww; J
}wwJ K
"wwK L
)wwL M
;wwM N
}xx 
publiczz 

voidzz 
GoToListMenuzz 
(zz 
intzz  

categoryIdzz! +
)zz+ ,
{{{ 
_navigation|| 
.|| 

NavigateTo|| 
(|| 
$"|| !
$str||! +
{||+ ,

categoryId||, 6
}||6 7
"||7 8
)||8 9
;||9 :
}}} 
public 

void 

GoToDetail 
( 
int 
courseId '
)' (
{
ÄÄ 
_navigation
ÅÅ 
.
ÅÅ 

NavigateTo
ÅÅ 
(
ÅÅ 
$"
ÅÅ !
$str
ÅÅ! )
{
ÅÅ) *
courseId
ÅÅ* 2
}
ÅÅ2 3
"
ÅÅ3 4
)
ÅÅ4 5
;
ÅÅ5 6
}
ÇÇ 
}ÉÉ 