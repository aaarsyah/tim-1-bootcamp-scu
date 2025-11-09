Æ®
:D:\BootcampProject\src\08.BlazorUI\Services\UserService.cs
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
,DDV W
GuidDDX \
	userRefIdDD] f
,DDf g
RoleRequestDtoDDh v
requestDDw ~
)DD~ 
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
{JJW X
	userRefIdJJX a
}JJa b
$strJJb l
"JJl m
,JJm n
requestJJo v
)JJv w
;JJw x
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
,YY[ \
GuidYY] a
	userRefIdYYb k
,YYk l
RoleRequestDtoYYm {
request	YY| É
)
YYÉ Ñ
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
{__W X
	userRefId__X a
}__a b
$str__b o
"__o p
,__p q
request__r y
)__y z
;__z {
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
,nnX Y
GuidnnZ ^
	userRefIdnn_ h
,nnh i
ClaimDtonnj r
requestnns z
)nnz {
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
{ttW X
	userRefIdttX a
}tta b
$strttb m
"ttm n
,ttn o
requestttp w
)ttw x
;ttx y
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
ÉÉ\ ]
Guid
ÉÉ^ b
	userRefId
ÉÉc l
,
ÉÉl m
ClaimDto
ÉÉn v
request
ÉÉw ~
)
ÉÉ~ 
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
ââW X
	userRefId
ââX a
}
ââa b
$str
ââb p
"
ââp q
,
ââq r
request
ââs z
)
ââz {
;
ââ{ |
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
òòU V
Guid
òòW [
	userRefId
òò\ e
)
òòe f
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
ûûW X
	userRefId
ûûX a
}
ûûa b
$str
ûûb k
"
ûûk l
,
ûûl m
new
ûûn q
{
ûûr s
}
ûût u
)
ûûu v
;
ûûv w
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
¨¨W X
Guid
¨¨Y ]
	userRefId
¨¨^ g
)
¨¨g h
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
≤≤W X
	userRefId
≤≤X a
}
≤≤a b
$str
≤≤b m
"
≤≤m n
,
≤≤n o
new
≤≤p s
{
≤≤t u
}
≤≤v w
)
≤≤w x
;
≤≤x y
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
}€€ ˜
=D:\BootcampProject\src\08.BlazorUI\Services\MyClassService.cs
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
., -
GetAsync- 5
(5 6
$str6 C
)C D
;D E
var 
rawJson 
= 
await 
response  (
.( )
Content) 0
.0 1
ReadAsStringAsync1 B
(B C
)C D
;D E
Console 
. 
	WriteLine 
( 
$"  
$str  /
{/ 0
rawJson0 7
}7 8
"8 9
)9 :
;: ;
if 
( 
! 
response 
. 
IsSuccessStatusCode -
)- .
{ 
Console 
. 
	WriteLine !
(! "
$"" $
$str$ :
{: ;
response; C
.C D

StatusCodeD N
}N O
"O P
)P Q
;Q R
return 
new 
( 
) 
; 
} 
var 
apiResponse 
= 
await #
response$ ,
., -
Content- 4
.4 5
ReadFromJsonAsync5 F
<F G
ApiResponseG R
<R S
ListS W
<W X

MyClassDtoX b
>b c
>c d
>d e
(e f
)f g
;g h
if   
(   
apiResponse   
?   
.   
Data   !
!=  " $
null  % )
)  ) *
{!! 
return"" 
apiResponse"" "
.""" #
Data""# '
.## 
OrderByDescending## &
(##& '
x##' (
=>##) +
x##, -
.##- .
Date##. 2
)##2 3
.$$ 
ToList$$ 
($$ 
)$$ 
??$$ 
new$$  
($$  !
)$$! "
;$$" #
}%% 
return'' 
new'' 
('' 
)'' 
;'' 
}(( 	
catch)) 
()) 
	Exception)) 
ex)) 
))) 
{** 	
Console++ 
.++ 
	WriteLine++ 
(++ 
$"++  
$str++  =
{++= >
ex++> @
.++@ A
Message++A H
}++H I
"++I J
)++J K
;++K L
return,, 
new,, 
(,, 
),, 
;,, 
}-- 	
}.. 
}// ¬
FD:\BootcampProject\src\08.BlazorUI\Services\Interfaces\IUserService.cs
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
I J
Guid

K O
	userRefId

P Y
,

Y Z
RoleRequestDto

[ i
request

j q
)

q r
;

r s
Task 
< 	
bool	 
> #
RemoveRoleFromUserAsync &
(& '%
AuthenticationHeaderValue' @
authorizationA N
,N O
GuidP T
	userRefIdU ^
,^ _
RoleRequestDto` n
requesto v
)v w
;w x
Task 
< 	
bool	 
>  
SetClaimForUserAsync #
(# $%
AuthenticationHeaderValue$ =
authorization> K
,K L
GuidM Q
	userRefIdR [
,[ \
ClaimDto] e
requestf m
)m n
;n o
Task 
< 	
bool	 
> $
RemoveClaimFromUserAsync '
(' (%
AuthenticationHeaderValue( A
authorizationB O
,O P
GuidQ U
	userRefIdV _
,_ `
ClaimDtoa i
requestj q
)q r
;r s
Task 
< 	
bool	 
> 
ActivateUserAsync  
(  !%
AuthenticationHeaderValue! :
authorization; H
,H I
GuidJ N
	userRefIdO X
)X Y
;Y Z
Task 
< 	
bool	 
> 
DeactivateUserAsync "
(" #%
AuthenticationHeaderValue# <
authorization= J
,J K
GuidL P
	userRefIdQ Z
)Z [
;[ \
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
} ∞
ID:\BootcampProject\src\08.BlazorUI\Services\Interfaces\IMyClassService.cs
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
}		 ›
HD:\BootcampProject\src\08.BlazorUI\Services\Interfaces\ICourseService.cs
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
(		' (
Guid		( ,
courseRefId		- 8
)		8 9
;		9 :
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
(+ ,
Guid, 0
categoryRefId1 >
)> ?
;? @
Task 
< 	
List	 
< 
	CourseDto 
> 
> $
GetCourseByCategoryAsync 2
(2 3
Guid3 7
categoryRefId8 E
)E F
;F G
} ∞
JD:\BootcampProject\src\08.BlazorUI\Services\Interfaces\ICheckoutService.cs
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
,		K L
Guid		M Q
scheduleRefId		R _
)		_ `
;		` a
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
K L
Guid

M Q
cartItemRefId

R _
)

_ `
;

` a
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
< 
PaymentMethodDto 
> 
>  
GetAllPaymentsAsync! 4
(4 5%
AuthenticationHeaderValue5 N
authorizationO \
)\ ]
;] ^
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
,V W
GuidX \
invoiceRefId] i
)i j
;j k
Task 
< 	

InvoiceDto	 
? 
> 
GetInvoiceByIdAsync )
() *%
AuthenticationHeaderValue* C
authorizationD Q
,Q R
GuidS W
invoiceRefIdX d
)d e
;e f
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
,o p
Guidq u
invoiceRefId	v Ç
)
Ç É
;
É Ñ
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
,j k
Guidl p
invoiceRefIdq }
)} ~
;~ 
} ã
FD:\BootcampProject\src\08.BlazorUI\Services\Interfaces\IAuthService.cs
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
Task 
LogoutAsync	 
( %
AuthenticationHeaderValue .
authorization/ <
)< =
;= >
Task 
< 	
bool	 
> 
IsLoggedInAsync 
( 
)  
;  !
Task 
< 	
bool	 
> 
IsAdminAsync 
( 
) 
; 
Task 
< 	%
AuthenticationHeaderValue	 "
?" #
># $
GetAuthAsync% 1
(1 2
)2 3
;3 4
} ≤
GD:\BootcampProject\src\08.BlazorUI\Services\Interfaces\IAdminService.cs
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
,		N O"
CreateCourseRequestDto		P f
request		g n
)		n o
;		o p
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
N O
Guid

P T
refId

U Z
,

Z ["
UpdateCourseRequestDto

\ r
request

s z
)

z {
;

{ |
Task 
< 	
bool	 
> 
DeleteCourseAsync  
(  !%
AuthenticationHeaderValue! :
authorization; H
,H I
GuidJ N
refIdO T
)T U
;U V
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
,R S$
CreateCategoryRequestDtoT l
requestm t
)t u
;u v
Task 
< 	
CategoryDto	 
? 
> 
UpdateCategoryAsync *
(* +%
AuthenticationHeaderValue+ D
authorizationE R
,R S
GuidT X
refIdY ^
,^ _$
UpdateCategoryRequestDto` x
request	y Ä
)
Ä Å
;
Å Ç
Task 
< 	
bool	 
> 
DeleteCategoryAsync "
(" #%
AuthenticationHeaderValue# <
authorization= J
,J K
GuidL P
refIdQ V
)V W
;W X
Task 
< 	
PaymentMethodDto	 
? 
> $
CreatePaymentMethodAsync 4
(4 5%
AuthenticationHeaderValue5 N
authorizationO \
,\ ])
CreatePaymentMethodRequestDto^ {
request	| É
)
É Ñ
;
Ñ Ö
Task 
< 	
PaymentMethodDto	 
? 
> $
UpdatePaymentMethodAsync 4
(4 5%
AuthenticationHeaderValue5 N
authorizationO \
,\ ]
Guid^ b
refIdc h
,h i*
UpdatePaymentMethodRequestDto	j á
request
à è
)
è ê
;
ê ë
Task 
< 	
bool	 
> $
DeletePaymentMethodAsync '
(' (%
AuthenticationHeaderValue( A
authorizationB O
,O P
GuidQ U
refIdV [
)[ \
;\ ]
} –M
;D:\BootcampProject\src\08.BlazorUI\Services\ErrorService.cs
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
new6 9!
ArgumentNullException: O
(O P
)P Q
;Q R
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
new< ?!
ArgumentNullException@ U
(U V
)V W
;W X
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
(   !
ArgumentNullException   ,
)  , -
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
new((6 9!
ArgumentNullException((: O
(((O P
)((P Q
;((Q R
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
new..< ?!
ArgumentNullException..@ U
(..U V
)..V W
;..W X
return00 
problemDetails00 )
.00) *
Title00* /
;00/ 0
}11 
catch22 
(22 !
ArgumentNullException22 ,
)22, -
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
new::6 9!
ArgumentNullException::: O
(::O P
)::P Q
;::Q R
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
newDD< ?!
ArgumentNullExceptionDD@ U
(DDU V
)DDV W
;DDW X
returnEE 
problemDetailsEE )
.EE) *
TitleEE* /
;EE/ 0
}FF 
catchGG 
(GG !
ArgumentNullExceptionGG ,
)GG, -
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
newOO6 9!
ArgumentNullExceptionOO: O
(OOO P
)OOP Q
;OOQ R
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
newUU< ?!
ArgumentNullExceptionUU@ U
(UUU V
)UUV W
;UUW X
returnVV 
problemDetailsVV )
.VV) *
TitleVV* /
;VV/ 0
}WW 
catchXX 
(XX !
ArgumentNullExceptionXX ,
)XX, -
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
new``6 9!
ArgumentNullException``: O
(``O P
)``P Q
;``Q R
ifaa 
(aa 
problemDetailsaa &
.aa& '
Titleaa' ,
==aa- /
nullaa0 4
)aa4 5
throwaa6 ;
newaa< ?!
ArgumentNullExceptionaa@ U
(aaU V
)aaV W
;aaW X
returnbb 
problemDetailsbb )
.bb) *
Titlebb* /
;bb/ 0
}cc 
catchdd 
(dd !
ArgumentNullExceptiondd ,
)dd, -
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
}ll ’5
FD:\BootcampProject\src\08.BlazorUI\Services\CustomAuthStateProvider.cs
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
}OO –ë
<D:\BootcampProject\src\08.BlazorUI\Services\CourseService.cs
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
.))0 1
ToList))1 7
())7 8
)))8 9
;))9 :
}** 
return++ 
null++ 
;++ 
},, 
return-- 
null-- 
;-- 
}.. 	
catch// 
(// 
	Exception// 
ex// 
)// 
{00 	
Console11 
.11 
	WriteLine11 
(11 
$"11  
$str11  9
{119 :
ex11: <
.11< =
Message11= D
}11D E
"11E F
)11F G
;11G H
return22 
null22 
;22 
}33 	
}44 
public55 

async55 
Task55 
<55 
PaginatedResponse55 '
<55' (
IEnumerable55( 3
<553 4
	CourseDto554 =
>55= >
>55> ?
?55? @
>55@ A&
GetAllCoursePaginatedAsync55B \
(55\ ]
int55] `
page55a e
,55e f
int55g j
pageSize55k s
)55s t
{66 
var88 
_httpClient88 
=88 
_factory88 "
.88" #
CreateClient88# /
(88/ 0
$str880 8
)888 9
;889 :
var:: 
	parameter:: 
=:: 
new:: !
CourseQueryParameters:: 1
(::1 2
)::2 3
;::3 4
var;; 
query;; 
=;; 
new;; 

Dictionary;; "
<;;" #
string;;# )
,;;) *
string;;+ 1
?;;1 2
>;;2 3
{<< 	
[== 
$str== 
]== 
=== 
page== !
.==! "
ToString==" *
(==* +
)==+ ,
,==, -
[>> 
$str>> 
]>> 
=>> 
pageSize>> #
.>># $
ToString>>$ ,
(>>, -
)>>- .
,>>. /
[?? 
$str?? 
]?? 
=?? 
	parameter?? "
.??" #
Search??# )
,??) *
[@@ 
$str@@ 
]@@ 
=@@ 
	parameter@@ &
.@@& '

CategoryId@@' 1
.@@1 2
ToString@@2 :
(@@: ;
)@@; <
,@@< =
[AA 
$strAA 
]AA 
=AA 
	parameterAA $
.AA$ %
MinPriceAA% -
.AA- .
ToStringAA. 6
(AA6 7
)AA7 8
,AA8 9
[BB 
$strBB 
]BB 
=BB 
	parameterBB $
.BB$ %
MaxPriceBB% -
.BB- .
ToStringBB. 6
(BB6 7
)BB7 8
,BB8 9
[CC 
$strCC 
]CC 
=CC 
	parameterCC "
.CC" #
SortByCC# )
,CC) *
[DD 
$strDD 
]DD 
=DD 
	parameterDD  )
.DD) *
SortDirectionDD* 7
,DD7 8
}FF 	
;FF	 

tryGG 
{HH 	
varII 
responseII 
=II 
awaitII  
_httpClientII! ,
.II, -
GetAsyncII- 5
(II5 6
QueryHelpersII6 B
.IIB C
AddQueryStringIIC Q
(IIQ R
$strIIR a
,IIa b
queryIIc h
)IIh i
)IIi j
;IIj k
ifJJ 
(JJ 
responseJJ 
.JJ 
IsSuccessStatusCodeJJ ,
)JJ, -
{KK 
varLL 
apiResponseLL 
=LL  !
awaitLL" '
responseLL( 0
.LL0 1
ContentLL1 8
.LL8 9
ReadFromJsonAsyncLL9 J
<LLJ K
ApiResponseLLK V
<LLV W
PaginatedResponseLLW h
<LLh i
IEnumerableLLi t
<LLt u
	CourseDtoLLu ~
>LL~ 
>	LL Ä
>
LLÄ Å
>
LLÅ Ç
(
LLÇ É
)
LLÉ Ñ
;
LLÑ Ö
ifNN 
(NN 
apiResponseNN 
?NN  
.NN  !
DataNN! %
!=NN& (
nullNN) -
)NN- .
{OO 
returnPP 
apiResponsePP &
.PP& '
DataPP' +
;PP+ ,
}QQ 
returnRR 
nullRR 
;RR 
}SS 
returnTT 
nullTT 
;TT 
}UU 	
catchVV 
(VV 
	ExceptionVV 
exVV 
)VV 
{WW 	
ConsoleXX 
.XX 
	WriteLineXX 
(XX 
$"XX  
$strXX  9
{XX9 :
exXX: <
.XX< =
MessageXX= D
}XXD E
"XXE F
)XXF G
;XXG H
returnYY 
nullYY 
;YY 
}ZZ 	
}[[ 
public\\ 

async\\ 
Task\\ 
<\\ 
	CourseDto\\ 
?\\  
>\\  !
GetCourseByIdAsync\\" 4
(\\4 5
Guid\\5 9
courseRefId\\: E
)\\E F
{]] 
var__ 
_httpClient__ 
=__ 
_factory__ "
.__" #
CreateClient__# /
(__/ 0
$str__0 8
)__8 9
;__9 :
try`` 
{aa 	
varbb 
responsebb 
=bb 
awaitbb  
_httpClientbb! ,
.bb, -
GetAsyncbb- 5
(bb5 6
$"bb6 8
$strbb8 C
{bbC D
courseRefIdbbD O
}bbO P
"bbP Q
)bbQ R
;bbR S
ifcc 
(cc 
responsecc 
.cc 
IsSuccessStatusCodecc ,
)cc, -
{dd 
varee 
apiResponseee 
=ee  !
awaitee" '
responseee( 0
.ee0 1
Contentee1 8
.ee8 9
ReadFromJsonAsyncee9 J
<eeJ K
ApiResponseeeK V
<eeV W
	CourseDtoeeW `
>ee` a
>eea b
(eeb c
)eec d
;eed e
ifgg 
(gg 
apiResponsegg 
?gg  
.gg  !
Datagg! %
!=gg& (
nullgg) -
)gg- .
{hh 
returnii 
apiResponseii &
.ii& '
Dataii' +
;ii+ ,
}jj 
returnkk 
nullkk 
;kk 
}ll 
returnmm 
nullmm 
;mm 
}nn 	
catchoo 
(oo 
	Exceptionoo 
exoo 
)oo 
{pp 	
Consoleqq 
.qq 
	WriteLineqq 
(qq 
$"qq  
$strqq  2
{qq2 3
exqq3 5
.qq5 6
Messageqq6 =
}qq= >
"qq> ?
)qq? @
;qq@ A
returnrr 
nullrr 
;rr 
}ss 	
}tt 
publicuu 

asyncuu 
Taskuu 
<uu 
Listuu 
<uu 
CategoryDtouu &
>uu& '
>uu' (!
GetAllCategoriesAsyncuu) >
(uu> ?
)uu? @
{vv 
varxx 
_httpClientxx 
=xx 
_factoryxx "
.xx" #
CreateClientxx# /
(xx/ 0
$strxx0 8
)xx8 9
;xx9 :
tryyy 
{zz 	
var{{ 
response{{ 
={{ 
await{{  
_httpClient{{! ,
.{{, -
GetAsync{{- 5
({{5 6
$str{{6 D
){{D E
;{{E F
if|| 
(|| 
response|| 
.|| 
IsSuccessStatusCode|| ,
)||, -
{}} 
var~~ 
apiResponse~~ 
=~~  !
await~~" '
response~~( 0
.~~0 1
Content~~1 8
.~~8 9
ReadFromJsonAsync~~9 J
<~~J K
ApiResponse~~K V
<~~V W
List~~W [
<~~[ \
CategoryDto~~\ g
>~~g h
>~~h i
>~~i j
(~~j k
)~~k l
;~~l m
if
ÄÄ 
(
ÄÄ 
apiResponse
ÄÄ 
?
ÄÄ  
.
ÄÄ  !
Data
ÄÄ! %
!=
ÄÄ& (
null
ÄÄ) -
)
ÄÄ- .
{
ÅÅ 
return
ÇÇ 
apiResponse
ÇÇ &
.
ÇÇ& '
Data
ÇÇ' +
;
ÇÇ+ ,
}
ÉÉ 
return
ÑÑ 
new
ÑÑ 
(
ÑÑ 
)
ÑÑ 
;
ÑÑ 
}
ÖÖ 
return
ÜÜ 
new
ÜÜ 
(
ÜÜ 
)
ÜÜ 
;
ÜÜ 
}
áá 	
catch
àà 
(
àà 
	Exception
àà 
ex
àà 
)
àà 
{
ââ 	
Console
ää 
.
ää 
	WriteLine
ää 
(
ää 
$"
ää  
$str
ää  9
{
ää9 :
ex
ää: <
.
ää< =
Message
ää= D
}
ääD E
"
ääE F
)
ääF G
;
ääG H
return
ãã 
new
ãã 
(
ãã 
)
ãã 
;
ãã 
}
åå 	
}
çç 
public
éé 

async
éé 
Task
éé 
<
éé 
CategoryDto
éé !
?
éé! "
>
éé" #"
GetCategoryByIdAsync
éé$ 8
(
éé8 9
Guid
éé9 =
categoryRefId
éé> K
)
ééK L
{
èè 
var
ëë 
_httpClient
ëë 
=
ëë 
_factory
ëë "
.
ëë" #
CreateClient
ëë# /
(
ëë/ 0
$str
ëë0 8
)
ëë8 9
;
ëë9 :
try
íí 
{
ìì 	
var
îî 
response
îî 
=
îî 
await
îî  
_httpClient
îî! ,
.
îî, -
GetAsync
îî- 5
(
îî5 6
$"
îî6 8
$str
îî8 E
{
îîE F
categoryRefId
îîF S
}
îîS T
"
îîT U
)
îîU V
;
îîV W
if
ïï 
(
ïï 
response
ïï 
.
ïï !
IsSuccessStatusCode
ïï ,
)
ïï, -
{
ññ 
var
óó 
apiResponse
óó 
=
óó  !
await
óó" '
response
óó( 0
.
óó0 1
Content
óó1 8
.
óó8 9
ReadFromJsonAsync
óó9 J
<
óóJ K
ApiResponse
óóK V
<
óóV W
CategoryDto
óóW b
>
óób c
>
óóc d
(
óód e
)
óóe f
;
óóf g
if
ôô 
(
ôô 
apiResponse
ôô 
?
ôô  
.
ôô  !
Data
ôô! %
!=
ôô& (
null
ôô) -
)
ôô- .
{
öö 
return
õõ 
apiResponse
õõ &
.
õõ& '
Data
õõ' +
;
õõ+ ,
}
úú 
return
ùù 
null
ùù 
;
ùù 
}
ûû 
return
üü 
null
üü 
;
üü 
}
†† 	
catch
°° 
(
°° 
	Exception
°° 
ex
°° 
)
°° 
{
¢¢ 	
Console
££ 
.
££ 
	WriteLine
££ 
(
££ 
$"
££  
$str
££  2
{
££2 3
ex
££3 5
.
££5 6
Message
££6 =
}
££= >
"
££> ?
)
££? @
;
££@ A
return
§§ 
null
§§ 
;
§§ 
}
•• 	
}
¶¶ 
public
®® 

async
®® 
Task
®® 
<
®® 
List
®® 
<
®® 
	CourseDto
®® $
>
®®$ %
>
®®% &&
GetCourseByCategoryAsync
®®' ?
(
®®? @
Guid
®®@ D
categoryRefId
®®E R
)
®®R S
{
©© 
var
™™ 
_httpClient
™™ 
=
™™ 
_factory
™™ "
.
™™" #
CreateClient
™™# /
(
™™/ 0
$str
™™0 8
)
™™8 9
;
™™9 :
try
´´ 
{
¨¨ 	
var
≠≠ 
response
≠≠ 
=
≠≠ 
await
≠≠  
_httpClient
≠≠! ,
.
≠≠, -
GetAsync
≠≠- 5
(
≠≠5 6
$"
≠≠6 8
$str
≠≠8 Q
{
≠≠Q R
categoryRefId
≠≠R _
}
≠≠_ `
"
≠≠` a
)
≠≠a b
;
≠≠b c
if
ØØ 
(
ØØ 
!
ØØ 
response
ØØ 
.
ØØ !
IsSuccessStatusCode
ØØ -
)
ØØ- .
{
∞∞ 
Console
±± 
.
±± 
	WriteLine
±± !
(
±±! "
$"
±±" $
$str
±±$ :
{
±±: ;
response
±±; C
.
±±C D

StatusCode
±±D N
}
±±N O
"
±±O P
)
±±P Q
;
±±Q R
return
≤≤ 
new
≤≤ 
List
≤≤ 
<
≤≤  
	CourseDto
≤≤  )
>
≤≤) *
(
≤≤* +
)
≤≤+ ,
;
≤≤, -
}
≥≥ 
var
µµ 
apiResponse
µµ 
=
µµ 
await
µµ #
response
µµ$ ,
.
µµ, -
Content
µµ- 4
.
µµ4 5
ReadFromJsonAsync
µµ5 F
<
µµF G
ApiResponse
µµG R
<
µµR S
PaginatedResponse
µµS d
<
µµd e
List
µµe i
<
µµi j
	CourseDto
µµj s
>
µµs t
>
µµt u
>
µµu v
>
µµv w
(
µµw x
)
µµx y
;
µµy z
if
∑∑ 
(
∑∑ 
apiResponse
∑∑ 
?
∑∑ 
.
∑∑ 
Data
∑∑ !
?
∑∑! "
.
∑∑" #
Data
∑∑# '
!=
∑∑( *
null
∑∑+ /
)
∑∑/ 0
{
∏∏ 
return
ππ 
apiResponse
ππ "
.
ππ" #
Data
ππ# '
.
ππ' (
Data
ππ( ,
.
∫∫ 
Where
∫∫ 
(
∫∫ 
c
∫∫ 
=>
∫∫ 
c
∫∫  !
.
∫∫! "
IsActive
∫∫" *
&&
∫∫+ -
c
∫∫. /
.
∫∫/ 0
CategoryRefId
∫∫0 =
==
∫∫> @
categoryRefId
∫∫A N
)
∫∫N O
.
ªª 
ToList
ªª 
(
ªª 
)
ªª 
;
ªª 
}
ºº 
return
ææ 
new
ææ 
List
ææ 
<
ææ 
	CourseDto
ææ %
>
ææ% &
(
ææ& '
)
ææ' (
;
ææ( )
}
øø 	
catch
¿¿ 
(
¿¿ 
	Exception
¿¿ 
ex
¿¿ 
)
¿¿ 
{
¡¡ 	
Console
¬¬ 
.
¬¬ 
	WriteLine
¬¬ 
(
¬¬ 
$"
¬¬  
$str
¬¬  2
{
¬¬2 3
ex
¬¬3 5
.
¬¬5 6
Message
¬¬6 =
}
¬¬= >
"
¬¬> ?
)
¬¬? @
;
¬¬@ A
return
√√ 
new
√√ 
List
√√ 
<
√√ 
	CourseDto
√√ %
>
√√% &
(
√√& '
)
√√' (
;
√√( )
}
ƒƒ 	
}
≈≈ 
}«« àÁ
>D:\BootcampProject\src\08.BlazorUI\Services\CheckoutService.cs
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
,((X Y
Guid((Z ^
scheduleRefId((_ l
)((l m
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
$str.. 
].. 
=.. 
scheduleRefId..  -
...- .
ToString... 6
(..6 7
)..7 8
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
,EEX Y
GuidEEZ ^
cartItemRefIdEE_ l
)EEl m
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
$strKK 
]KK 
=KK 
cartItemRefIdKK  -
.KK- .
ToStringKK. 6
(KK6 7
)KK7 8
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
<|| 
PaymentMethodDto|| +
>||+ ,
>||, -
GetAllPaymentsAsync||. A
(||A B%
AuthenticationHeaderValue||B [
authorization||\ i
)||i j
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
ÖÖ[ \
PaymentMethodDto
ÖÖ\ l
>
ÖÖl m
>
ÖÖm n
>
ÖÖn o
(
ÖÖo p
)
ÖÖp q
;
ÖÖq r
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
ÀÀc d
Guid
ÀÀe i
invoiceRefId
ÀÀj v
)
ÀÀv w
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
——J K
invoiceRefId
——K W
}
——W X
"
——X Y
)
——Y Z
;
——Z [
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
ÂÂ^ _
Guid
ÂÂ` d
invoiceRefId
ÂÂe q
)
ÂÂq r
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
ÎÎD E
invoiceRefId
ÎÎE Q
}
ÎÎQ R
"
ÎÎR S
)
ÎÎS T
;
ÎÎT U
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
ˇˇ| }
Guidˇˇ~ Ç
invoiceRefIdˇˇÉ è
)ˇˇè ê
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
ÖÖP Q
invoiceRefId
ÖÖQ ]
}
ÖÖ] ^
"
ÖÖ^ _
)
ÖÖ_ `
;
ÖÖ` a
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
òòw x
Guid
òòy }
invoiceRefIdòò~ ä
)òòä ã
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
ûûJ K
invoiceRefId
ûûK W
}
ûûW X
"
ûûX Y
)
ûûY Z
;
ûûZ [
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
}±± çæ
:D:\BootcampProject\src\08.BlazorUI\Services\AuthService.cs
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
readonly #
JwtSecurityTokenHandler ,$
_jwtSecurityTokenHandler- E
=F G
newH K
(K L
)L M
;M N
public 

AuthService 
( 
IHttpClientFactory 
factory "
," # 
ILocalStorageService 
localStorage )
,) *'
AuthenticationStateProvider #
authStateProvider$ 5
)5 6
{ 
_factory 
= 
factory 
; 
_localStorage 
= 
localStorage $
;$ %
} 
public 

async 
Task 
< 
AuthResponseDto %
?% &
>& '

LoginAsync( 2
(2 3
LoginRequestDto3 B
requestC J
)J K
{ 
var 
_httpClient 
= 
_factory "
." #
CreateClient# /
(/ 0
$str0 8
)8 9
;9 :
try 
{ 	
var 
response 
= 
await  
_httpClient! ,
., -
PostAsJsonAsync- <
(< =
$str= M
,M N
requestO V
)V W
;W X
if   
(   
response   
.   
IsSuccessStatusCode   ,
)  , -
{!! 
var"" 
apiResponse"" 
=""  !
await""" '
response""( 0
.""0 1
Content""1 8
.""8 9
ReadFromJsonAsync""9 J
<""J K
ApiResponse""K V
<""V W
AuthResponseDto""W f
>""f g
>""g h
(""h i
)""i j
;""j k
if$$ 
($$ 
apiResponse$$ 
?$$  
.$$  !
Data$$! %
!=$$& (
null$$) -
)$$- .
{%% 
await&& 
SetTokensAsync&& (
(&&( )
apiResponse&&) 4
.&&4 5
Data&&5 9
.&&9 :
AccessToken&&: E
,&&E F
apiResponse&&G R
.&&R S
Data&&S W
.&&W X
RefreshToken&&X d
)&&d e
;&&e f
_httpClient(( 
.((  !
DefaultRequestHeaders((  5
.((5 6
Authorization((6 C
=((D E
new)) %
AuthenticationHeaderValue)) 5
())5 6
$str))6 >
,))> ?
apiResponse))@ K
.))K L
Data))L P
.))P Q
AccessToken))Q \
)))\ ]
;))] ^
return-- 
apiResponse-- &
.--& '
Data--' +
;--+ ,
}.. 
}// 
return11 
null11 
;11 
}22 	
catch33 
{44 	
return55 
null55 
;55 
}66 	
}77 
public88 

async88 
Task88 
<88 
bool88 
>88 
RegisterAsync88 )
(88) *
RegisterRequestDto88* <
request88= D
)88D E
{99 
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
try;; 
{<< 	
var== 
response== 
=== 
await==  
_httpClient==! ,
.==, -
PostAsJsonAsync==- <
(==< =
$str=== P
,==P Q
request==R Y
)==Y Z
;==Z [
if>> 
(>> 
!>> 
response>> 
.>> 
IsSuccessStatusCode>> -
)>>- .
{?? 
var@@ 
problemDetails@@ "
=@@# $
await@@% *
response@@+ 3
.@@3 4
Content@@4 ;
.@@; <
ReadFromJsonAsync@@< M
<@@M N
ProblemDetails@@N \
>@@\ ]
(@@] ^
)@@^ _
;@@_ `
ifAA 
(AA 
problemDetailsAA "
==AA# %
nullAA& *
)AA* +
{BB 
returnDD 
falseDD  
;DD  !
}EE 
returnFF 
falseFF 
;FF 
}GG 
varHH 
apiResponseHH 
=HH 
awaitHH #
responseHH$ ,
.HH, -
ContentHH- 4
.HH4 5
ReadFromJsonAsyncHH5 F
<HHF G
ApiResponseHHG R
<HHR S
objectHHS Y
>HHY Z
>HHZ [
(HH[ \
)HH\ ]
;HH] ^
returnII 
apiResponseII 
!=II !
nullII" &
;II& '
}JJ 	
catchKK 
{LL 	
returnMM 
falseMM 
;MM 
}NN 	
}OO 
publicPP 

asyncPP 
TaskPP 
<PP 
boolPP 
>PP 
ConfirmEmailAsyncPP -
(PP- ."
ConfirmEmailRequestDtoPP. D
requestPPE L
)PPL M
{QQ 
varRR 
_httpClientRR 
=RR 
_factoryRR "
.RR" #
CreateClientRR# /
(RR/ 0
$strRR0 8
)RR8 9
;RR9 :
trySS 
{TT 	
varUU 
responseUU 
=UU 
awaitUU  
_httpClientUU! ,
.UU, -
PostAsJsonAsyncUU- <
(UU< =
$strUU= U
,UUU V
requestUUW ^
)UU^ _
;UU_ `
ifWW 
(WW 
!WW 
responseWW 
.WW 
IsSuccessStatusCodeWW -
)WW- .
{XX 
returnYY 
falseYY 
;YY 
}ZZ 
var[[ 
apiResponse[[ 
=[[ 
await[[ #
response[[$ ,
.[[, -
Content[[- 4
.[[4 5
ReadFromJsonAsync[[5 F
<[[F G
ApiResponse[[G R
<[[R S
object[[S Y
>[[Y Z
>[[Z [
([[[ \
)[[\ ]
;[[] ^
return\\ 
apiResponse\\ 
!=\\ !
null\\" &
;\\& '
}]] 	
catch^^ 
{__ 	
return`` 
false`` 
;`` 
}aa 	
}bb 
publiccc 

asynccc 
Taskcc 
<cc 
boolcc 
>cc 
ChangePasswordAsynccc /
(cc/ 0%
AuthenticationHeaderValuecc0 I
authorizationccJ W
,ccW X$
ChangePasswordRequestDtoccY q
requestccr y
)ccy z
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
$strii= W
,iiW X
requestiiY `
)ii` a
;iia b
ifkk 
(kk 
!kk 
responsekk 
.kk 
IsSuccessStatusCodekk -
)kk- .
{ll 
returnmm 
falsemm 
;mm 
}nn 
varoo 
apiResponseoo 
=oo 
awaitoo #
responseoo$ ,
.oo, -
Contentoo- 4
.oo4 5
ReadFromJsonAsyncoo5 F
<ooF G
ApiResponseooG R
<ooR S
objectooS Y
>ooY Z
>ooZ [
(oo[ \
)oo\ ]
;oo] ^
returnpp 
apiResponsepp 
!=pp !
nullpp" &
;pp& '
}qq 	
catchrr 
{ss 	
returntt 
falsett 
;tt 
}uu 	
}vv 
publicww 

asyncww 
Taskww 
<ww 
boolww 
>ww 
ForgotPasswordAsyncww /
(ww/ 0$
ForgotPasswordRequestDtoww0 H
requestwwI P
)wwP Q
{xx 
varyy 
_httpClientyy 
=yy 
_factoryyy "
.yy" #
CreateClientyy# /
(yy/ 0
$stryy0 8
)yy8 9
;yy9 :
tryzz 
{{{ 	
var|| 
response|| 
=|| 
await||  
_httpClient||! ,
.||, -
PostAsJsonAsync||- <
(||< =
$str||= W
,||W X
request||Y `
)||` a
;||a b
if~~ 
(~~ 
!~~ 
response~~ 
.~~ 
IsSuccessStatusCode~~ -
)~~- .
{ 
return
ÄÄ 
false
ÄÄ 
;
ÄÄ 
}
ÅÅ 
var
ÇÇ 
apiResponse
ÇÇ 
=
ÇÇ 
await
ÇÇ #
response
ÇÇ$ ,
.
ÇÇ, -
Content
ÇÇ- 4
.
ÇÇ4 5
ReadFromJsonAsync
ÇÇ5 F
<
ÇÇF G
ApiResponse
ÇÇG R
<
ÇÇR S
object
ÇÇS Y
>
ÇÇY Z
>
ÇÇZ [
(
ÇÇ[ \
)
ÇÇ\ ]
;
ÇÇ] ^
return
ÉÉ 
apiResponse
ÉÉ 
!=
ÉÉ !
null
ÉÉ" &
;
ÉÉ& '
}
ÑÑ 	
catch
ÖÖ 
{
ÜÜ 	
return
áá 
false
áá 
;
áá 
}
àà 	
}
ââ 
public
ää 

async
ää 
Task
ää 
<
ää 
bool
ää 
>
ää  
ResetPasswordAsync
ää .
(
ää. /%
ResetPasswordRequestDto
ää/ F
request
ääG N
)
ääN O
{
ãã 
var
åå 
_httpClient
åå 
=
åå 
_factory
åå "
.
åå" #
CreateClient
åå# /
(
åå/ 0
$str
åå0 8
)
åå8 9
;
åå9 :
try
çç 
{
éé 	
var
èè 
response
èè 
=
èè 
await
èè  
_httpClient
èè! ,
.
èè, -
PostAsJsonAsync
èè- <
(
èè< =
$str
èè= V
,
èèV W
request
èèX _
)
èè_ `
;
èè` a
if
ëë 
(
ëë 
!
ëë 
response
ëë 
.
ëë !
IsSuccessStatusCode
ëë -
)
ëë- .
{
íí 
return
ìì 
false
ìì 
;
ìì 
}
îî 
var
ïï 
apiResponse
ïï 
=
ïï 
await
ïï #
response
ïï$ ,
.
ïï, -
Content
ïï- 4
.
ïï4 5
ReadFromJsonAsync
ïï5 F
<
ïïF G
ApiResponse
ïïG R
<
ïïR S
object
ïïS Y
>
ïïY Z
>
ïïZ [
(
ïï[ \
)
ïï\ ]
;
ïï] ^
return
ññ 
apiResponse
ññ 
!=
ññ !
null
ññ" &
;
ññ& '
}
óó 	
catch
òò 
{
ôô 	
return
öö 
false
öö 
;
öö 
}
õõ 	
}
úú 
public
ùù 

async
ùù 
Task
ùù 
LogoutAsync
ùù !
(
ùù! "'
AuthenticationHeaderValue
ùù" ;
authorization
ùù< I
)
ùùI J
{
ûû 
var
üü 
_httpClient
üü 
=
üü 
_factory
üü "
.
üü" #
CreateClient
üü# /
(
üü/ 0
$str
üü0 8
)
üü8 9
;
üü9 :
_httpClient
†† 
.
†† #
DefaultRequestHeaders
†† )
.
††) *
Authorization
††* 7
=
††8 9
authorization
††: G
;
††G H
try
°° 
{
¢¢ 	
var
££ 
response
££ 
=
££ 
await
££  
_httpClient
££! ,
.
££, -
	PostAsync
££- 6
(
££6 7
$str
££7 H
,
££H I
null
££J N
)
££N O
;
££O P
if
•• 
(
•• 
!
•• 
response
•• 
.
•• !
IsSuccessStatusCode
•• -
)
••- .
{
¶¶ 
await
®® 
ClearTokensAsync
®® &
(
®®& '
)
®®' (
;
®®( )
return
´´ 
;
´´ 
}
¨¨ 
var
≠≠ 
apiResponse
≠≠ 
=
≠≠ 
await
≠≠ #
response
≠≠$ ,
.
≠≠, -
Content
≠≠- 4
.
≠≠4 5
ReadFromJsonAsync
≠≠5 F
<
≠≠F G
ApiResponse
≠≠G R
<
≠≠R S
UserDto
≠≠S Z
>
≠≠Z [
>
≠≠[ \
(
≠≠\ ]
)
≠≠] ^
;
≠≠^ _
if
ÆÆ 
(
ÆÆ 
apiResponse
ÆÆ 
==
ÆÆ 
null
ÆÆ #
)
ÆÆ# $
{
ØØ 
await
±± 
ClearTokensAsync
±± &
(
±±& '
)
±±' (
;
±±( )
return
¥¥ 
;
¥¥ 
}
µµ 
await
∑∑ 
ClearTokensAsync
∑∑ "
(
∑∑" #
)
∑∑# $
;
∑∑$ %
return
∫∫ 
;
∫∫ 
}
ªª 	
catch
ºº 
{
ΩΩ 	
return
øø 
;
øø 
}
¿¿ 	
}
¬¬ 
private
√√ 
async
√√ 
Task
√√ 
<
√√ 
string
√√ 
?
√√ 
>
√√ !
GetAccessTokenAsync
√√  3
(
√√3 4
)
√√4 5
{
ƒƒ 
var
∆∆ 
token
∆∆ 
=
∆∆ 
await
∆∆ 
_localStorage
∆∆ '
.
∆∆' (
GetItemAsync
∆∆( 4
<
∆∆4 5
string
∆∆5 ;
>
∆∆; <
(
∆∆< =
$str
∆∆= H
)
∆∆H I
;
∆∆I J
if
…… 

(
…… 
string
…… 
.
……  
IsNullOrWhiteSpace
…… %
(
……% &
token
……& +
)
……+ ,
)
……, -
{
   	
await
ÀÀ 
ClearTokensAsync
ÀÀ "
(
ÀÀ" #
)
ÀÀ# $
;
ÀÀ$ %
return
ÃÃ 
null
ÃÃ 
;
ÃÃ 
}
ÕÕ 	
var
œœ 
expiry
œœ 
=
œœ &
_jwtSecurityTokenHandler
œœ -
.
œœ- .
ReadJwtToken
œœ. :
(
œœ: ;
token
œœ; @
)
œœ@ A
.
œœA B
Claims
œœB H
.
œœH I
FirstOrDefault
œœI W
(
œœW X
c
œœX Y
=>
œœZ \
c
œœ] ^
.
œœ^ _
Type
œœ_ c
==
œœd f
$str
œœg l
)
œœl m
?
œœm n
.
œœn o
Value
œœo t
;
œœt u
if
–– 

(
–– 
expiry
–– 
==
–– 
null
–– 
)
–– 
{
—— 	
await
““ 
ClearTokensAsync
““ "
(
““" #
)
““# $
;
““$ %
return
”” 
null
”” 
;
”” 
}
‘‘ 	
var
÷÷ 
expiryDateTime
÷÷ 
=
÷÷ 
DateTimeOffset
÷÷ +
.
÷÷+ ,!
FromUnixTimeSeconds
÷÷, ?
(
÷÷? @
long
÷÷@ D
.
÷÷D E
Parse
÷÷E J
(
÷÷J K
expiry
÷÷K Q
)
÷÷Q R
)
÷÷R S
;
÷÷S T
Console
ŸŸ 
.
ŸŸ 
	WriteLine
ŸŸ 
(
ŸŸ 
$"
ŸŸ 
$str
ŸŸ !
{
ŸŸ! "
DateTimeOffset
ŸŸ" 0
.
ŸŸ0 1
UtcNow
ŸŸ1 7
}
ŸŸ7 8
$str
ŸŸ8 H
{
ŸŸH I
DateTimeOffset
ŸŸI W
.
ŸŸW X
UtcNow
ŸŸX ^
.
ŸŸ^ _

AddMinutes
ŸŸ_ i
(
ŸŸi j
$num
ŸŸj k
)
ŸŸk l
}
ŸŸl m
$str
ŸŸm |
{
ŸŸ| }
expiryDateTimeŸŸ} ã
}ŸŸã å
"ŸŸå ç
)ŸŸç é
;ŸŸé è
if
‹‹ 

(
‹‹ 
expiryDateTime
‹‹ 
<
‹‹ 
DateTimeOffset
‹‹ +
.
‹‹+ ,
UtcNow
‹‹, 2
.
‹‹2 3

AddMinutes
‹‹3 =
(
‹‹= >
$num
‹‹> ?
)
‹‹? @
)
‹‹@ A
{
›› 	
var
ﬂﬂ 
refreshToken
ﬂﬂ 
=
ﬂﬂ 
await
ﬂﬂ $
_localStorage
ﬂﬂ% 2
.
ﬂﬂ2 3
GetItemAsync
ﬂﬂ3 ?
<
ﬂﬂ? @
string
ﬂﬂ@ F
>
ﬂﬂF G
(
ﬂﬂG H
$str
ﬂﬂH V
)
ﬂﬂV W
;
ﬂﬂW X
if
‡‡ 
(
‡‡ 
string
‡‡ 
.
‡‡  
IsNullOrWhiteSpace
‡‡ )
(
‡‡) *
refreshToken
‡‡* 6
)
‡‡6 7
)
‡‡7 8
{
·· 
await
‚‚ 
ClearTokensAsync
‚‚ &
(
‚‚& '
)
‚‚' (
;
‚‚( )
return
„„ 
null
„„ 
;
„„ 
}
‰‰ 
var
ÊÊ 
_httpClient
ÊÊ 
=
ÊÊ 
_factory
ÊÊ &
.
ÊÊ& '
CreateClient
ÊÊ' 3
(
ÊÊ3 4
$str
ÊÊ4 <
)
ÊÊ< =
;
ÊÊ= >
try
ÁÁ 
{
ËË 
var
ÈÈ 
request
ÈÈ 
=
ÈÈ 
new
ÈÈ !$
RefreshTokenRequestDto
ÈÈ" 8
{
ÍÍ 
AccessToken
ÎÎ 
=
ÎÎ  !
token
ÎÎ" '
,
ÎÎ' (
RefreshToken
ÏÏ  
=
ÏÏ! "
refreshToken
ÏÏ# /
}
ÌÌ 
;
ÌÌ 
var
ÓÓ 
response
ÓÓ 
=
ÓÓ 
await
ÓÓ $
_httpClient
ÓÓ% 0
.
ÓÓ0 1
PostAsJsonAsync
ÓÓ1 @
(
ÓÓ@ A
$str
ÓÓA Y
,
ÓÓY Z
request
ÓÓ[ b
)
ÓÓb c
;
ÓÓc d
if
 
(
 
!
 
response
 
.
 !
IsSuccessStatusCode
 1
)
1 2
{
ÒÒ 
await
ÚÚ 
ClearTokensAsync
ÚÚ *
(
ÚÚ* +
)
ÚÚ+ ,
;
ÚÚ, -
return
ÛÛ 
null
ÛÛ 
;
ÛÛ  
}
ÙÙ 
var
ıı 
apiResponse
ıı 
=
ıı  !
await
ıı" '
response
ıı( 0
.
ıı0 1
Content
ıı1 8
.
ıı8 9
ReadFromJsonAsync
ıı9 J
<
ııJ K
ApiResponse
ııK V
<
ııV W
AuthResponseDto
ııW f
>
ııf g
>
ııg h
(
ııh i
)
ııi j
;
ııj k
if
˜˜ 
(
˜˜ 
apiResponse
˜˜ 
?
˜˜  
.
˜˜  !
Data
˜˜! %
==
˜˜& (
null
˜˜) -
)
˜˜- .
{
¯¯ 
await
˘˘ 
ClearTokensAsync
˘˘ *
(
˘˘* +
)
˘˘+ ,
;
˘˘, -
return
˙˙ 
null
˙˙ 
;
˙˙  
}
˚˚ 
await
¸¸ 
SetTokensAsync
¸¸ $
(
¸¸$ %
apiResponse
¸¸% 0
.
¸¸0 1
Data
¸¸1 5
.
¸¸5 6
AccessToken
¸¸6 A
,
¸¸A B
apiResponse
¸¸C N
.
¸¸N O
Data
¸¸O S
.
¸¸S T
RefreshToken
¸¸T `
)
¸¸` a
;
¸¸a b
return
˝˝ 
apiResponse
˝˝ "
.
˝˝" #
Data
˝˝# '
.
˝˝' (
AccessToken
˝˝( 3
;
˝˝3 4
}
˛˛ 
catch
ˇˇ 
{
ÄÄ 
await
ÅÅ 
ClearTokensAsync
ÅÅ &
(
ÅÅ& '
)
ÅÅ' (
;
ÅÅ( )
return
ÇÇ 
null
ÇÇ 
;
ÇÇ 
}
ÉÉ 
}
ÑÑ 	
return
ÖÖ 
token
ÖÖ 
;
ÖÖ 
}
ÜÜ 
public
áá 

async
áá 
Task
áá 
<
áá 
bool
áá 
>
áá 
IsLoggedInAsync
áá +
(
áá+ ,
)
áá, -
{
àà 
return
ââ 
await
ââ !
GetAccessTokenAsync
ââ (
(
ââ( )
)
ââ) *
!=
ââ+ -
null
ââ. 2
;
ââ2 3
}
ää 
public
ãã 

async
ãã 
Task
ãã 
<
ãã 
bool
ãã 
>
ãã 
IsAdminAsync
ãã (
(
ãã( )
)
ãã) *
{
åå 
var
çç 
token
çç 
=
çç 
await
çç !
GetAccessTokenAsync
çç -
(
çç- .
)
çç. /
;
çç/ 0
if
éé 

(
éé 
token
éé 
==
éé 
null
éé 
)
éé 
{
èè 	
return
êê 
false
êê 
;
êê 
}
ëë 	
var
íí 
role
íí 
=
íí &
_jwtSecurityTokenHandler
íí +
.
íí+ ,
ReadJwtToken
íí, 8
(
íí8 9
token
íí9 >
)
íí> ?
.
íí? @
Claims
íí@ F
.
ííF G
FirstOrDefault
ííG U
(
ííU V
c
ííV W
=>
ííX Z
c
íí[ \
.
íí\ ]
Type
íí] a
==
ííb d
$str
ííe k
)
íík l
?
ííl m
.
íím n
Value
íín s
;
íís t
if
ìì 

(
ìì 
role
ìì 
==
ìì 
null
ìì 
)
ìì 
{
îî 	
return
ïï 
false
ïï 
;
ïï 
}
ññ 	
Console
óó 
.
óó 
	WriteLine
óó 
(
óó 
$"
óó 
$str
óó '
{
óó' (
role
óó( ,
}
óó, -
"
óó- .
)
óó. /
;
óó/ 0
return
òò 
role
òò 
.
òò 
Contains
òò 
(
òò 
$str
òò $
)
òò$ %
;
òò% &
}
ôô 
public
†† 

async
†† 
Task
†† 
<
†† '
AuthenticationHeaderValue
†† /
?
††/ 0
>
††0 1
GetAuthAsync
††2 >
(
††> ?
)
††? @
{
°° 
var
¢¢ 
a
¢¢ 
=
¢¢ 
await
¢¢ !
GetAccessTokenAsync
¢¢ )
(
¢¢) *
)
¢¢* +
;
¢¢+ ,
if
££ 

(
££ 
a
££ 
==
££ 
null
££ 
)
££ 
{
§§ 	
return
•• 
null
•• 
;
•• 
}
¶¶ 	
return
ßß 
new
ßß '
AuthenticationHeaderValue
ßß ,
(
ßß, -
$str
ßß- 5
,
ßß5 6
a
ßß7 8
)
ßß8 9
;
ßß9 :
}
®® 
private
©© 
async
©© 
Task
©© 
ClearTokensAsync
©© '
(
©©' (
)
©©( )
{
™™ 
await
´´ 
_localStorage
´´ 
.
´´ 
RemoveItemAsync
´´ +
(
´´+ ,
$str
´´, 7
)
´´7 8
;
´´8 9
await
¨¨ 
_localStorage
¨¨ 
.
¨¨ 
RemoveItemAsync
¨¨ +
(
¨¨+ ,
$str
¨¨, :
)
¨¨: ;
;
¨¨; <
}
≠≠ 
private
ÆÆ 
async
ÆÆ 
Task
ÆÆ 
SetTokensAsync
ÆÆ %
(
ÆÆ% &
string
ÆÆ& ,
accessToken
ÆÆ- 8
,
ÆÆ8 9
string
ÆÆ: @
refreshToken
ÆÆA M
)
ÆÆM N
{
ØØ 
await
∞∞ 
_localStorage
∞∞ 
.
∞∞ 
SetItemAsync
∞∞ (
(
∞∞( )
$str
∞∞) 4
,
∞∞4 5
accessToken
∞∞6 A
)
∞∞A B
;
∞∞B C
await
±± 
_localStorage
±± 
.
±± 
SetItemAsync
±± (
(
±±( )
$str
±±) 7
,
±±7 8
refreshToken
±±9 E
)
±±E F
;
±±F G
}
≤≤ 
}≥≥ àÙ
;D:\BootcampProject\src\08.BlazorUI\Services\AdminService.cs
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
.**0 1
ToList**1 7
(**7 8
)**8 9
;**9 :
}++ 
return,, 
new,, 
(,, 
),, 
;,, 
}-- 
return.. 
new.. 
(.. 
).. 
;.. 
}// 	
catch00 
(00 
	Exception00 
ex00 
)00 
{11 	
Console22 
.22 
	WriteLine22 
(22 
$"22  
$str22  :
{22: ;
ex22; =
.22= >
Message22> E
}22E F
"22F G
)22G H
;22H I
return33 
new33 
(33 
)33 
;33 
}44 	
}55 
public88 

async88 
Task88 
<88 
	CourseDto88 
?88  
>88  !
CreateCourseAsync88" 3
(883 4%
AuthenticationHeaderValue884 M
authorization88N [
,88[ \"
CreateCourseRequestDto88] s
request88t {
)88{ |
{99 
var:: 
_httpClient:: 
=:: 
_factory:: "
.::" #
CreateClient::# /
(::/ 0
$str::0 8
)::8 9
;::9 :
_httpClient;; 
.;; !
DefaultRequestHeaders;; )
.;;) *
Authorization;;* 7
=;;8 9
authorization;;: G
;;;G H
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
$str>>= I
,>>I J
request>>K R
)>>R S
;>>S T
if?? 
(?? 
response?? 
.?? 
IsSuccessStatusCode?? ,
)??, -
{@@ 
varAA 
apiResponseAA 
=AA  !
awaitAA" '
responseAA( 0
.AA0 1
ContentAA1 8
.AA8 9
ReadFromJsonAsyncAA9 J
<AAJ K
ApiResponseAAK V
<AAV W
	CourseDtoAAW `
>AA` a
>AAa b
(AAb c
)AAc d
;AAd e
ifCC 
(CC 
apiResponseCC 
?CC  
.CC  !
DataCC! %
!=CC& (
nullCC) -
)CC- .
{DD 
returnEE 
apiResponseEE &
.EE& '
DataEE' +
;EE+ ,
}FF 
returnGG 
nullGG 
;GG 
}HH 
returnII 
nullII 
;II 
}JJ 	
catchKK 
(KK 
	ExceptionKK 
exKK 
)KK 
{LL 	
ConsoleMM 
.MM 
	WriteLineMM 
(MM 
$"MM  
$strMM  :
{MM: ;
exMM; =
.MM= >
MessageMM> E
}MME F
"MMF G
)MMG H
;MMH I
returnNN 
nullNN 
;NN 
}OO 	
}PP 
publicRR 

asyncRR 
TaskRR 
<RR 
	CourseDtoRR 
?RR  
>RR  !
UpdateCourseAsyncRR" 3
(RR3 4%
AuthenticationHeaderValueRR4 M
authorizationRRN [
,RR[ \
GuidRR] a
refIdRRb g
,RRg h"
UpdateCourseRequestDtoRRi 
request
RRÄ á
)
RRá à
{SS 
varTT 
_httpClientTT 
=TT 
_factoryTT "
.TT" #
CreateClientTT# /
(TT/ 0
$strTT0 8
)TT8 9
;TT9 :
_httpClientUU 
.UU !
DefaultRequestHeadersUU )
.UU) *
AuthorizationUU* 7
=UU8 9
authorizationUU: G
;UUG H
tryWW 
{XX 	
varYY 
responseYY 
=YY 
awaitYY  
_httpClientYY! ,
.YY, -
PutAsJsonAsyncYY- ;
(YY; <
$"YY< >
$strYY> I
{YYI J
refIdYYJ O
}YYO P
"YYP Q
,YYQ R
requestYYS Z
)YYZ [
;YY[ \
ConsoleZZ 
.ZZ 
	WriteLineZZ 
(ZZ 
$"ZZ  
$strZZ  *
{ZZ* +
responseZZ+ 3
.ZZ3 4
ContentZZ4 ;
.ZZ; <
ReadAsStringAsyncZZ< M
(ZZM N
)ZZN O
.ZZO P
ResultZZP V
}ZZV W
"ZZW X
)ZZX Y
;ZZY Z
if[[ 
([[ 
response[[ 
.[[ 
IsSuccessStatusCode[[ ,
)[[, -
{\\ 
var]] 
apiResponse]] 
=]]  !
await]]" '
response]]( 0
.]]0 1
Content]]1 8
.]]8 9
ReadFromJsonAsync]]9 J
<]]J K
ApiResponse]]K V
<]]V W
	CourseDto]]W `
>]]` a
>]]a b
(]]b c
)]]c d
;]]d e
if__ 
(__ 
apiResponse__ 
?__  
.__  !
Data__! %
!=__& (
null__) -
)__- .
{`` 
returnaa 
apiResponseaa &
.aa& '
Dataaa' +
;aa+ ,
}bb 
returncc 
nullcc 
;cc 
}dd 
returnee 
nullee 
;ee 
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
$strii  :
{ii: ;
exii; =
.ii= >
Messageii> E
}iiE F
"iiF G
)iiG H
;iiH I
returnjj 
nulljj 
;jj 
}kk 	
}ll 
publicnn 

asyncnn 
Tasknn 
<nn 
boolnn 
>nn 
DeleteCourseAsyncnn -
(nn- .%
AuthenticationHeaderValuenn. G
authorizationnnH U
,nnU V
GuidnnW [
refIdnn\ a
)nna b
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
tryss 
{tt 	
varuu 
responseuu 
=uu 
awaituu  
_httpClientuu! ,
.uu, -
DeleteAsyncuu- 8
(uu8 9
$"uu9 ;
$struu; F
{uuF G
refIduuG L
}uuL M
"uuM N
)uuN O
;uuO P
ifvv 
(vv 
!vv 
responsevv 
.vv 
IsSuccessStatusCodevv -
)vv- .
{ww 
returnxx 
falsexx 
;xx 
}yy 
varzz 
apiResponsezz 
=zz 
awaitzz #
responsezz$ ,
.zz, -
Contentzz- 4
.zz4 5
ReadFromJsonAsynczz5 F
<zzF G
ApiResponsezzG R
<zzR S
objectzzS Y
>zzY Z
>zzZ [
(zz[ \
)zz\ ]
;zz] ^
return{{ 
apiResponse{{ 
!={{ !
null{{" &
;{{& '
}|| 	
catch}} 
(}} 
	Exception}} 
ex}} 
)}} 
{~~ 	
Console 
. 
	WriteLine 
( 
$"  
$str  :
{: ;
ex; =
.= >
Message> E
}E F
"F G
)G H
;H I
return
ÄÄ 
false
ÄÄ 
;
ÄÄ 
}
ÅÅ 	
}
ÇÇ 
public
ÑÑ 

async
ÑÑ 
Task
ÑÑ 
<
ÑÑ 
List
ÑÑ 
<
ÑÑ 
CategoryDto
ÑÑ &
>
ÑÑ& '
>
ÑÑ' (!
GetAllCategoryAsync
ÑÑ) <
(
ÑÑ< =
)
ÑÑ= >
{
ÖÖ 
var
ÜÜ 
_httpClient
ÜÜ 
=
ÜÜ 
_factory
ÜÜ "
.
ÜÜ" #
CreateClient
ÜÜ# /
(
ÜÜ/ 0
$str
ÜÜ0 8
)
ÜÜ8 9
;
ÜÜ9 :
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
ââ, -
GetAsync
ââ- 5
(
ââ5 6
$str
ââ6 D
)
ââD E
;
ââE F
if
ää 
(
ää 
response
ää 
.
ää !
IsSuccessStatusCode
ää ,
)
ää, -
{
ãã 
var
åå 
apiResponse
åå 
=
åå  !
await
åå" '
response
åå( 0
.
åå0 1
Content
åå1 8
.
åå8 9
ReadFromJsonAsync
åå9 J
<
ååJ K
ApiResponse
ååK V
<
ååV W
List
ååW [
<
åå[ \
CategoryDto
åå\ g
>
ååg h
>
ååh i
>
ååi j
(
ååj k
)
ååk l
;
åål m
if
éé 
(
éé 
apiResponse
éé 
?
éé  
.
éé  !
Data
éé! %
!=
éé& (
null
éé) -
)
éé- .
{
èè 
return
êê 
apiResponse
êê &
.
êê& '
Data
êê' +
;
êê+ ,
}
ëë 
return
íí 
new
íí 
(
íí 
)
íí 
;
íí 
}
ìì 
return
îî 
new
îî 
(
îî 
)
îî 
;
îî 
}
ïï 	
catch
ññ 
(
ññ 
	Exception
ññ 
ex
ññ 
)
ññ 
{
óó 	
Console
òò 
.
òò 
	WriteLine
òò 
(
òò 
$"
òò  
$str
òò  B
{
òòB C
ex
òòC E
.
òòE F
Message
òòF M
}
òòM N
"
òòN O
)
òòO P
;
òòP Q
return
ôô 
new
ôô 
(
ôô 
)
ôô 
;
ôô 
}
öö 	
}
õõ 
public
ûû 

async
ûû 
Task
ûû 
<
ûû 
CategoryDto
ûû !
?
ûû! "
>
ûû" #!
CreateCategoryAsync
ûû$ 7
(
ûû7 8'
AuthenticationHeaderValue
ûû8 Q
authorization
ûûR _
,
ûû_ `&
CreateCategoryRequestDto
ûûa y
requestûûz Å
)ûûÅ Ç
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
§§, -
PostAsJsonAsync
§§- <
(
§§< =
$str
§§= K
,
§§K L
request
§§M T
)
§§T U
;
§§U V
if
•• 
(
•• 
response
•• 
.
•• !
IsSuccessStatusCode
•• ,
)
••, -
{
¶¶ 
var
ßß 
apiResponse
ßß 
=
ßß  !
await
ßß" '
response
ßß( 0
.
ßß0 1
Content
ßß1 8
.
ßß8 9
ReadFromJsonAsync
ßß9 J
<
ßßJ K
ApiResponse
ßßK V
<
ßßV W
CategoryDto
ßßW b
>
ßßb c
>
ßßc d
(
ßßd e
)
ßße f
;
ßßf g
if
©© 
(
©© 
apiResponse
©© 
?
©©  
.
©©  !
Data
©©! %
!=
©©& (
null
©©) -
)
©©- .
{
™™ 
return
´´ 
apiResponse
´´ &
.
´´& '
Data
´´' +
;
´´+ ,
}
¨¨ 
return
≠≠ 
null
≠≠ 
;
≠≠ 
}
ÆÆ 
return
ØØ 
null
ØØ 
;
ØØ 
}
∞∞ 	
catch
±± 
(
±± 
	Exception
±± 
ex
±± 
)
±± 
{
≤≤ 	
Console
≥≥ 
.
≥≥ 
	WriteLine
≥≥ 
(
≥≥ 
$"
≥≥  
$str
≥≥  :
{
≥≥: ;
ex
≥≥; =
.
≥≥= >
Message
≥≥> E
}
≥≥E F
"
≥≥F G
)
≥≥G H
;
≥≥H I
return
¥¥ 
null
¥¥ 
;
¥¥ 
}
µµ 	
}
∂∂ 
public
∏∏ 

async
∏∏ 
Task
∏∏ 
<
∏∏ 
CategoryDto
∏∏ !
?
∏∏! "
>
∏∏" #!
UpdateCategoryAsync
∏∏$ 7
(
∏∏7 8'
AuthenticationHeaderValue
∏∏8 Q
authorization
∏∏R _
,
∏∏_ `
Guid
∏∏a e
refId
∏∏f k
,
∏∏k l'
UpdateCategoryRequestDto∏∏m Ö
request∏∏Ü ç
)∏∏ç é
{
ππ 
var
∫∫ 
_httpClient
∫∫ 
=
∫∫ 
_factory
∫∫ "
.
∫∫" #
CreateClient
∫∫# /
(
∫∫/ 0
$str
∫∫0 8
)
∫∫8 9
;
∫∫9 :
_httpClient
ªª 
.
ªª #
DefaultRequestHeaders
ªª )
.
ªª) *
Authorization
ªª* 7
=
ªª8 9
authorization
ªª: G
;
ªªG H
try
ΩΩ 
{
ææ 	
var
øø 
response
øø 
=
øø 
await
øø  
_httpClient
øø! ,
.
øø, -
PutAsJsonAsync
øø- ;
(
øø; <
$"
øø< >
$str
øø> K
{
øøK L
refId
øøL Q
}
øøQ R
"
øøR S
,
øøS T
request
øøU \
)
øø\ ]
;
øø] ^
if
¿¿ 
(
¿¿ 
response
¿¿ 
.
¿¿ !
IsSuccessStatusCode
¿¿ ,
)
¿¿, -
{
¡¡ 
var
¬¬ 
apiResponse
¬¬ 
=
¬¬  !
await
¬¬" '
response
¬¬( 0
.
¬¬0 1
Content
¬¬1 8
.
¬¬8 9
ReadFromJsonAsync
¬¬9 J
<
¬¬J K
ApiResponse
¬¬K V
<
¬¬V W
CategoryDto
¬¬W b
>
¬¬b c
>
¬¬c d
(
¬¬d e
)
¬¬e f
;
¬¬f g
if
ƒƒ 
(
ƒƒ 
apiResponse
ƒƒ 
?
ƒƒ  
.
ƒƒ  !
Data
ƒƒ! %
!=
ƒƒ& (
null
ƒƒ) -
)
ƒƒ- .
{
≈≈ 
return
∆∆ 
apiResponse
∆∆ &
.
∆∆& '
Data
∆∆' +
;
∆∆+ ,
}
«« 
return
»» 
null
»» 
;
»» 
}
…… 
return
   
null
   
;
   
}
ÀÀ 	
catch
ÃÃ 
(
ÃÃ 
	Exception
ÃÃ 
ex
ÃÃ 
)
ÃÃ 
{
ÕÕ 	
Console
ŒŒ 
.
ŒŒ 
	WriteLine
ŒŒ 
(
ŒŒ 
$"
ŒŒ  
$str
ŒŒ  :
{
ŒŒ: ;
ex
ŒŒ; =
.
ŒŒ= >
Message
ŒŒ> E
}
ŒŒE F
"
ŒŒF G
)
ŒŒG H
;
ŒŒH I
return
œœ 
null
œœ 
;
œœ 
}
–– 	
}
—— 
public
”” 

async
”” 
Task
”” 
<
”” 
bool
”” 
>
”” !
DeleteCategoryAsync
”” /
(
””/ 0'
AuthenticationHeaderValue
””0 I
authorization
””J W
,
””W X
Guid
””Y ]
refId
””^ c
)
””c d
{
‘‘ 
var
’’ 
_httpClient
’’ 
=
’’ 
_factory
’’ "
.
’’" #
CreateClient
’’# /
(
’’/ 0
$str
’’0 8
)
’’8 9
;
’’9 :
_httpClient
÷÷ 
.
÷÷ #
DefaultRequestHeaders
÷÷ )
.
÷÷) *
Authorization
÷÷* 7
=
÷÷8 9
authorization
÷÷: G
;
÷÷G H
try
ÿÿ 
{
ŸŸ 	
var
⁄⁄ 
response
⁄⁄ 
=
⁄⁄ 
await
⁄⁄  
_httpClient
⁄⁄! ,
.
⁄⁄, -
DeleteAsync
⁄⁄- 8
(
⁄⁄8 9
$"
⁄⁄9 ;
$str
⁄⁄; H
{
⁄⁄H I
refId
⁄⁄I N
}
⁄⁄N O
"
⁄⁄O P
)
⁄⁄P Q
;
⁄⁄Q R
if
€€ 
(
€€ 
!
€€ 
response
€€ 
.
€€ !
IsSuccessStatusCode
€€ -
)
€€- .
{
‹‹ 
return
›› 
false
›› 
;
›› 
}
ﬁﬁ 
var
ﬂﬂ 
apiResponse
ﬂﬂ 
=
ﬂﬂ 
await
ﬂﬂ #
response
ﬂﬂ$ ,
.
ﬂﬂ, -
Content
ﬂﬂ- 4
.
ﬂﬂ4 5
ReadFromJsonAsync
ﬂﬂ5 F
<
ﬂﬂF G
ApiResponse
ﬂﬂG R
<
ﬂﬂR S
object
ﬂﬂS Y
>
ﬂﬂY Z
>
ﬂﬂZ [
(
ﬂﬂ[ \
)
ﬂﬂ\ ]
;
ﬂﬂ] ^
return
‡‡ 
apiResponse
‡‡ 
!=
‡‡ !
null
‡‡" &
;
‡‡& '
}
·· 	
catch
‚‚ 
(
‚‚ 
	Exception
‚‚ 
ex
‚‚ 
)
‚‚ 
{
„„ 	
Console
‰‰ 
.
‰‰ 
	WriteLine
‰‰ 
(
‰‰ 
$"
‰‰  
$str
‰‰  :
{
‰‰: ;
ex
‰‰; =
.
‰‰= >
Message
‰‰> E
}
‰‰E F
"
‰‰F G
)
‰‰G H
;
‰‰H I
return
ÂÂ 
false
ÂÂ 
;
ÂÂ 
}
ÊÊ 	
}
ÁÁ 
public
ÈÈ 

async
ÈÈ 
Task
ÈÈ 
<
ÈÈ 
List
ÈÈ 
<
ÈÈ 
PaymentMethodDto
ÈÈ +
>
ÈÈ+ ,
>
ÈÈ, -'
GetAllPaymentMethodsAsync
ÈÈ. G
(
ÈÈG H
)
ÈÈH I
{
ÍÍ 
var
ÎÎ 
_httpClient
ÎÎ 
=
ÎÎ 
_factory
ÎÎ "
.
ÎÎ" #
CreateClient
ÎÎ# /
(
ÎÎ/ 0
$str
ÎÎ0 8
)
ÎÎ8 9
;
ÎÎ9 :
try
ÏÏ 
{
ÌÌ 	
var
ÓÓ 
response
ÓÓ 
=
ÓÓ 
await
ÓÓ  
_httpClient
ÓÓ! ,
.
ÓÓ, -
GetAsync
ÓÓ- 5
(
ÓÓ5 6
$str
ÓÓ6 C
)
ÓÓC D
;
ÓÓD E
if
ÔÔ 
(
ÔÔ 
response
ÔÔ 
.
ÔÔ !
IsSuccessStatusCode
ÔÔ ,
)
ÔÔ, -
{
 
var
ÒÒ 
apiResponse
ÒÒ 
=
ÒÒ  !
await
ÒÒ" '
response
ÒÒ( 0
.
ÒÒ0 1
Content
ÒÒ1 8
.
ÒÒ8 9
ReadFromJsonAsync
ÒÒ9 J
<
ÒÒJ K
ApiResponse
ÒÒK V
<
ÒÒV W
List
ÒÒW [
<
ÒÒ[ \
PaymentMethodDto
ÒÒ\ l
>
ÒÒl m
>
ÒÒm n
>
ÒÒn o
(
ÒÒo p
)
ÒÒp q
;
ÒÒq r
if
ÛÛ 
(
ÛÛ 
apiResponse
ÛÛ 
?
ÛÛ  
.
ÛÛ  !
Data
ÛÛ! %
!=
ÛÛ& (
null
ÛÛ) -
)
ÛÛ- .
{
ÙÙ 
return
ıı 
apiResponse
ıı &
.
ıı& '
Data
ıı' +
;
ıı+ ,
}
ˆˆ 
return
˜˜ 
new
˜˜ 
(
˜˜ 
)
˜˜ 
;
˜˜ 
}
¯¯ 
return
˘˘ 
new
˘˘ 
(
˘˘ 
)
˘˘ 
;
˘˘ 
}
˙˙ 	
catch
˚˚ 
(
˚˚ 
	Exception
˚˚ 
ex
˚˚ 
)
˚˚ 
{
¸¸ 	
Console
˝˝ 
.
˝˝ 
	WriteLine
˝˝ 
(
˝˝ 
$"
˝˝  
$str
˝˝  B
{
˝˝B C
ex
˝˝C E
.
˝˝E F
Message
˝˝F M
}
˝˝M N
"
˝˝N O
)
˝˝O P
;
˝˝P Q
return
˛˛ 
new
˛˛ 
(
˛˛ 
)
˛˛ 
;
˛˛ 
}
ˇˇ 	
}
ÄÄ 
public
ÇÇ 

async
ÇÇ 
Task
ÇÇ 
<
ÇÇ 
PaymentMethodDto
ÇÇ &
?
ÇÇ& '
>
ÇÇ' (&
CreatePaymentMethodAsync
ÇÇ) A
(
ÇÇA B'
AuthenticationHeaderValue
ÇÇB [
authorization
ÇÇ\ i
,
ÇÇi j,
CreatePaymentMethodRequestDtoÇÇk à
requestÇÇâ ê
)ÇÇê ë
{
ÉÉ 
var
ÑÑ 
_httpClient
ÑÑ 
=
ÑÑ 
_factory
ÑÑ "
.
ÑÑ" #
CreateClient
ÑÑ# /
(
ÑÑ/ 0
$str
ÑÑ0 8
)
ÑÑ8 9
;
ÑÑ9 :
_httpClient
ÖÖ 
.
ÖÖ #
DefaultRequestHeaders
ÖÖ )
.
ÖÖ) *
Authorization
ÖÖ* 7
=
ÖÖ8 9
authorization
ÖÖ: G
;
ÖÖG H
try
ÜÜ 
{
áá 	
var
àà 
response
àà 
=
àà 
await
àà  
_httpClient
àà! ,
.
àà, -
PostAsJsonAsync
àà- <
(
àà< =
$str
àà= J
,
ààJ K
request
ààL S
)
ààS T
;
ààT U
if
ââ 
(
ââ 
response
ââ 
.
ââ !
IsSuccessStatusCode
ââ ,
)
ââ, -
{
ää 
var
ãã 
apiResponse
ãã 
=
ãã  !
await
ãã" '
response
ãã( 0
.
ãã0 1
Content
ãã1 8
.
ãã8 9
ReadFromJsonAsync
ãã9 J
<
ããJ K
ApiResponse
ããK V
<
ããV W
PaymentMethodDto
ããW g
>
ããg h
>
ããh i
(
ããi j
)
ããj k
;
ããk l
if
çç 
(
çç 
apiResponse
çç 
?
çç  
.
çç  !
Data
çç! %
!=
çç& (
null
çç) -
)
çç- .
{
éé 
return
èè 
apiResponse
èè &
.
èè& '
Data
èè' +
;
èè+ ,
}
êê 
return
ëë 
null
ëë 
;
ëë 
}
íí 
return
ìì 
null
ìì 
;
ìì 
}
îî 	
catch
ïï 
(
ïï 
	Exception
ïï 
ex
ïï 
)
ïï 
{
ññ 	
Console
óó 
.
óó 
	WriteLine
óó 
(
óó 
$"
óó  
$str
óó  A
{
óóA B
ex
óóB D
.
óóD E
Message
óóE L
}
óóL M
"
óóM N
)
óóN O
;
óóO P
return
òò 
null
òò 
;
òò 
}
ôô 	
}
öö 
public
úú 

async
úú 
Task
úú 
<
úú 
PaymentMethodDto
úú &
?
úú& '
>
úú' (&
UpdatePaymentMethodAsync
úú) A
(
úúA B'
AuthenticationHeaderValue
úúB [
authorization
úú\ i
,
úúi j
Guid
úúk o
refId
úúp u
,
úúu v,
UpdatePaymentMethodRequestDtoúúw î
requestúúï ú
)úúú ù
{
ùù 
var
ûû 
_httpClient
ûû 
=
ûû 
_factory
ûû "
.
ûû" #
CreateClient
ûû# /
(
ûû/ 0
$str
ûû0 8
)
ûû8 9
;
ûû9 :
_httpClient
üü 
.
üü #
DefaultRequestHeaders
üü )
.
üü) *
Authorization
üü* 7
=
üü8 9
authorization
üü: G
;
üüG H
try
°° 
{
¢¢ 	
var
££ 
response
££ 
=
££ 
await
££  
_httpClient
££! ,
.
££, -
PutAsJsonAsync
££- ;
(
££; <
$"
££< >
$str
££> J
{
££J K
refId
££K P
}
££P Q
"
££Q R
,
££R S
request
££T [
)
££[ \
;
££\ ]
if
§§ 
(
§§ 
response
§§ 
.
§§ !
IsSuccessStatusCode
§§ ,
)
§§, -
{
•• 
var
¶¶ 
apiResponse
¶¶ 
=
¶¶  !
await
¶¶" '
response
¶¶( 0
.
¶¶0 1
Content
¶¶1 8
.
¶¶8 9
ReadFromJsonAsync
¶¶9 J
<
¶¶J K
ApiResponse
¶¶K V
<
¶¶V W
PaymentMethodDto
¶¶W g
>
¶¶g h
>
¶¶h i
(
¶¶i j
)
¶¶j k
;
¶¶k l
if
®® 
(
®® 
apiResponse
®® 
?
®®  
.
®®  !
Data
®®! %
!=
®®& (
null
®®) -
)
®®- .
{
©© 
return
™™ 
apiResponse
™™ &
.
™™& '
Data
™™' +
;
™™+ ,
}
´´ 
return
¨¨ 
null
¨¨ 
;
¨¨ 
}
≠≠ 
return
ÆÆ 
null
ÆÆ 
;
ÆÆ 
}
ØØ 	
catch
∞∞ 
(
∞∞ 
	Exception
∞∞ 
ex
∞∞ 
)
∞∞ 
{
±± 	
Console
≤≤ 
.
≤≤ 
	WriteLine
≤≤ 
(
≤≤ 
$"
≤≤  
$str
≤≤  A
{
≤≤A B
ex
≤≤B D
.
≤≤D E
Message
≤≤E L
}
≤≤L M
"
≤≤M N
)
≤≤N O
;
≤≤O P
return
≥≥ 
null
≥≥ 
;
≥≥ 
}
¥¥ 	
}
µµ 
public
∑∑ 

async
∑∑ 
Task
∑∑ 
<
∑∑ 
bool
∑∑ 
>
∑∑ &
DeletePaymentMethodAsync
∑∑ 4
(
∑∑4 5'
AuthenticationHeaderValue
∑∑5 N
authorization
∑∑O \
,
∑∑\ ]
Guid
∑∑^ b
refId
∑∑c h
)
∑∑h i
{
∏∏ 
var
ππ 
_httpClient
ππ 
=
ππ 
_factory
ππ "
.
ππ" #
CreateClient
ππ# /
(
ππ/ 0
$str
ππ0 8
)
ππ8 9
;
ππ9 :
_httpClient
∫∫ 
.
∫∫ #
DefaultRequestHeaders
∫∫ )
.
∫∫) *
Authorization
∫∫* 7
=
∫∫8 9
authorization
∫∫: G
;
∫∫G H
try
ºº 
{
ΩΩ 	
var
ææ 
response
ææ 
=
ææ 
await
ææ  
_httpClient
ææ! ,
.
ææ, -
DeleteAsync
ææ- 8
(
ææ8 9
$"
ææ9 ;
$str
ææ; G
{
ææG H
refId
ææH M
}
ææM N
"
ææN O
)
ææO P
;
ææP Q
if
øø 
(
øø 
!
øø 
response
øø 
.
øø !
IsSuccessStatusCode
øø -
)
øø- .
{
¿¿ 
return
¡¡ 
false
¡¡ 
;
¡¡ 
}
¬¬ 
var
√√ 
apiResponse
√√ 
=
√√ 
await
√√ #
response
√√$ ,
.
√√, -
Content
√√- 4
.
√√4 5
ReadFromJsonAsync
√√5 F
<
√√F G
ApiResponse
√√G R
<
√√R S
object
√√S Y
>
√√Y Z
>
√√Z [
(
√√[ \
)
√√\ ]
;
√√] ^
return
ƒƒ 
apiResponse
ƒƒ 
!=
ƒƒ !
null
ƒƒ" &
;
ƒƒ& '
}
≈≈ 	
catch
∆∆ 
(
∆∆ 
	Exception
∆∆ 
ex
∆∆ 
)
∆∆ 
{
«« 	
Console
»» 
.
»» 
	WriteLine
»» 
(
»» 
$"
»»  
$str
»»  A
{
»»A B
ex
»»B D
.
»»D E
Message
»»E L
}
»»L M
"
»»M N
)
»»N O
;
»»O P
return
…… 
false
…… 
;
…… 
}
   	
}
ÀÀ 
}ÃÃ ∆&
-D:\BootcampProject\src\08.BlazorUI\Program.cs
var 
builder 
= 
WebApplication 
. 
CreateBuilder *
(* +
args+ /
)/ 0
;0 1
builder 
. 
Logging 
. 
	AddFilter 
( 
$str A
,A B
LogLevelC K
.K L
NoneL P
)P Q
;Q R
builder 
. 
Logging 
. 
	AddFilter 
( 
$str >
,> ?
LogLevel@ H
.H I
NoneI M
)M N
;N O
builder 
. 
Services 
. 
AddMudServices 
(  
)  !
;! "
builder 
. 
Services 
. 
AddRazorComponents #
(# $
)$ %
. *
AddInteractiveServerComponents #
(# $
)$ %
;% &
builder 
. 
Services 
. #
AddBlazoredLocalStorage (
(( )
)) *
;* +
builder 
. 
Services 
. 
AddHttpClient 
( 
$str '
,' (
sp) +
=>, .
{ 
sp 
. 
BaseAddress 
= 
new 
Uri 
( 
builder $
.$ %
Configuration% 2
[2 3
$str3 ?
]? @
??A C
$strD \
)\ ]
;] ^
} 
) 
; 
builder   
.   
Services   
.   
	AddScoped   
<    
NavigationManagerExt   /
>  / 0
(  0 1
)  1 2
;  2 3
builder## 
.## 
Services## 
.## 
	AddScoped## 
<## 
IUserService## '
,##' (
UserService##) 4
>##4 5
(##5 6
)##6 7
;##7 8
builder$$ 
.$$ 
Services$$ 
.$$ 
	AddScoped$$ 
<$$ 
ICheckoutService$$ +
,$$+ ,
CheckoutService$$- <
>$$< =
($$= >
)$$> ?
;$$? @
builder%% 
.%% 
Services%% 
.%% 
	AddScoped%% 
<%% 
IAdminService%% (
,%%( )
AdminService%%* 6
>%%6 7
(%%7 8
)%%8 9
;%%9 :
builder)) 
.)) 
Services)) 
.)) 
	AddScoped)) 
<)) 
IAuthService)) '
,))' (
AuthService))) 4
>))4 5
())5 6
)))6 7
;))7 8
builder** 
.** 
Services** 
.** 
	AddScoped** 
<** 
IMyClassService** *
,*** +
MyClassService**, :
>**: ;
(**; <
)**< =
;**= >
builder,, 
.,, 
Services,, 
.,, 
	AddScoped,, 
<,, 
ICourseService,, )
,,,) *
CourseService,,+ 8
>,,8 9
(,,9 :
),,: ;
;,,; <
builder// 
.// 
Services// 
.// 
	AddScoped// 
<// 
ErrorService// '
,//' (
ErrorService//) 5
>//5 6
(//6 7
)//7 8
;//8 9
var11 
app11 
=11 	
builder11
 
.11 
Build11 
(11 
)11 
;11 
if44 
(44 
!44 
app44 
.44 	
Environment44	 
.44 
IsDevelopment44 "
(44" #
)44# $
)44$ %
{55 
app66 
.66 
UseExceptionHandler66 
(66 
$str66 $
,66$ % 
createScopeForErrors66& :
:66: ;
true66< @
)66@ A
;66A B
app88 
.88 
UseHsts88 
(88 
)88 
;88 
}99 
app;; 
.;; 
UseHttpsRedirection;; 
(;; 
);; 
;;; 
app<< 
.<< 
UseAntiforgery<< 
(<< 
)<< 
;<< 
app>> 
.>> 
MapStaticAssets>> 
(>> 
)>> 
;>> 
app?? 
.?? 
MapRazorComponents?? 
<?? 
App?? 
>?? 
(?? 
)?? 
.@@ *
AddInteractiveServerRenderMode@@ #
(@@# $
)@@$ %
;@@% &
appBB 
.BB 
RunBB 
(BB 
)BB 	
;BB	 
¡8
KD:\BootcampProject\src\08.BlazorUI\Components\Pages\NavigationManagerExt.cs
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
(RR" #
GuidRR# '
invoiceRefIdRR( 4
)RR4 5
{SS 
_navigationTT 
.TT 

NavigateToTT 
(TT 
$"TT !
$strTT! 2
{TT2 3
invoiceRefIdTT3 ?
}TT? @
"TT@ A
)TTA B
;TTB C
}UU 
publicWW 

voidWW #
GoToDetailsInvoiceAdminWW '
(WW' (
GuidWW( ,
invoiceRefIdWW- 9
)WW9 :
{XX 
_navigationYY 
.YY 

NavigateToYY 
(YY 
$"YY !
$strYY! 8
{YY8 9
invoiceRefIdYY9 E
}YYE F
"YYF G
)YYG H
;YYH I
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
(zz 
Guidzz !
categoryRefIdzz" /
)zz/ 0
{{{ 
_navigation|| 
.|| 

NavigateTo|| 
(|| 
$"|| !
$str||! +
{||+ ,
categoryRefId||, 9
}||9 :
"||: ;
)||; <
;||< =
}}} 
public 

void 

GoToDetail 
( 
Guid 
courseRefId  +
)+ ,
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
ÅÅ) *
courseRefId
ÅÅ* 5
}
ÅÅ5 6
"
ÅÅ6 7
)
ÅÅ7 8
;
ÅÅ8 9
}
ÇÇ 
}ÉÉ 