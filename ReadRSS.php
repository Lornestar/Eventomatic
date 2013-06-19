<?php
 
// Make sure SimplePie is included. You may need to change this to match the location of simplepie.inc.
require_once('php/simplepie.inc');
 
// We'll process this feed with all of the default options.
$feed = new SimplePie("http://www.thegroupstore.com/rss/" . $_GET['storeid'] . ".xml");?>
 //document.writeln('<?php echo "http://www.thegroupstore.com/rss/" . $_GET['storeid'] . ".xml";?>');
 <?php
// This makes sure that the content is sent to the browser as text/html and the UTF-8 character set (since we didn't change it).
$feed->enable_cache(false);
$feed->init();
$feed->handle_content_type();
 
// Let's begin our XHTML webpage code.  The DOCTYPE is supposed to be the very first thing, so we'll keep it on the same line as the closing-PHP tag.
?>
document.writeln('');
 
document.writeln('<table class=Groupstoretable >');	
	<?php
	
	/*
	Here, we'll loop through all of the items in the feed, and $item represents the current item in the loop.
	*/
	foreach ($feed->get_items() as $item):
	    
	    $data = $item->get_item_tags('', 'comments');
	    
	?>

<?php $badstrings = array("\n", "'"); 
if ( $item->get_title() != "Currently no Events Selling" ) {
?>

var strtemp = '<?php echo $item->get_permalink(); ?>';
document.writeln('<tr valign=top><td class=Groupstoretextbox>');
document.writeln('<span class=GroupstoreTitle><a href='+strtemp.replace("%3D", '=')+'>');
document.writeln('<?php echo str_replace($badstrings,"",$item->get_title()); ?></a></span><br/>');
document.writeln('<span class=GroustoreDate><?php echo $item->get_date('j F Y | g:i a'); ?></span><br/>');
document.writeln('<span class=GroustoreDesc><?php echo  str_replace($badstrings, "" ,$item->get_description()); ?></span>');
document.writeln('</td><td><img src=<?php echo $data[0]['data']; ?> class=Groupstoreimg /></td>'); 
document.writeln('</tr>');
	<?php 
	}
	 endforeach; ?>

document.writeln('<tr><td align=right colspan=2><table><tr valign=middle><td>Powered by</td><td><a href=http://www.theGroupstore.com target=_blank><img src=http://www.theGroupstore.com/Images/groupstore_Order_Logo2.png style=border-style:none;height:25px; /></a></td></tr></table></td></tr>');
document.writeln('</table>');